using Common.Enum;
using Common.Queries;
using Dapper;
using Database;
using Entities.LogProcess;
using Models.DeviceControl;
using Models.InstrumentSetThreshold;
using Service.Contracts;
using Service.Contracts.DeviceThreshold;
using Service.Contracts.Logger;

namespace Service.DeviceThreshold
{
    public sealed class DeviceJobInstrumentationService : IDeviceJobInstrumentationService
    {
        private readonly DapperContext dapperContext;
        private readonly ILoggerManager loggerManager;
        private readonly IDeviceControlService deviceControlService;
        private readonly IDataStatisticsService dataStatisticsService;
        private static List<LogDeviceStatusEntity> logDeviceStatusEntities = new List<LogDeviceStatusEntity>();

        public DeviceJobInstrumentationService(DapperContext dapperContext,
            ILoggerManager loggerManager,
            IDeviceControlService deviceControlService, IDataStatisticsService dataStatisticsService)
        {
            this.dapperContext = dapperContext;
            this.loggerManager = loggerManager;
            this.deviceControlService = deviceControlService;
            this.dataStatisticsService = dataStatisticsService;
        }

        public async Task<bool> RunningJobThreshold(Guid deviceId, string valueString, string typeDevice)
        {
            loggerManager.LogInformation("Start running job threshold");
            var query = InstrumentationSetThresholdQuery.GetThresholdByInstrumentationId;

            IEnumerable<InstrumentationGetForSystem> result;
            int value = -1000;
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                result = await connection.QueryAsync<InstrumentationGetForSystem>(query, new { InstrumentationId = deviceId, TypeDevice = typeDevice });
                connection.Close();
            }
            bool successT = int.TryParse(valueString, out value);
            bool success = false;
            if (result.Any() && successT)
            {
                foreach (var item in result)
                {
                    if (item.OnInUpperThreshold)
                    {
                        if (item.ThresholdValueOn < value && !item.DeviceDriverAction)
                        {
                            // Logic mở thiết bị điều khiển
                            success = await TurnOnOffDevice(item.ModuleDeviceDrId, item.DeviceDriverId, true, DeviceName: item.NameDeviceDriver, thresholdId: item.Id);
                        }
                        else if (item.ThresholdValueOff > value && item.DeviceDriverAction)
                        {
                            // Logic đóng thiết bị điều khiển
                            success = await TurnOnOffDevice(item.ModuleDeviceDrId, item.DeviceDriverId, false, DeviceName: item.NameDeviceDriver, thresholdId: item.Id);
                        }
                    }
                    else
                    {
                        if (item.ThresholdValueOn < value && item.DeviceDriverAction)
                        {
                            // Logic đóng thiết bị điều khiển 
                            success = await TurnOnOffDevice(item.ModuleDeviceDrId, item.DeviceDriverId, false, DeviceName: item.NameDeviceDriver, thresholdId: item.Id);
                        }
                        else if (item.ThresholdValueOff > value && !item.DeviceDriverAction)
                        {
                            // Logic mở thiết bị điều khiển
                            success = await TurnOnOffDevice(item.ModuleDeviceDrId, item.DeviceDriverId, true, DeviceName: item.NameDeviceDriver, thresholdId: item.Id);
                        }
                    }
                }
            }
            await dataStatisticsService.PushDataLogDeviceOnOff(logDeviceStatusEntities);
            loggerManager.LogInformation("End running job threshold");
            return await Task.FromResult(success);
        }
        private async Task<bool> TurnOnOffDevice(Guid ModuleId, Guid DeviceId, bool isTurnOn, string? DeviceName, int? thresholdId)
        {
            loggerManager.LogInformation($"Off Device {DeviceId}");
            var model = new OnOffDeviceQueryModel()
            {
                ModuleId = ModuleId,
                DeviceId = DeviceId,
                RequestOn = isTurnOn,
            };
            var IsComplete = await deviceControlService.DeviceDriverOnOff(model);
            // ghi log đóng thiết bị tự động theo threshold

            logDeviceStatusEntities.Add(new LogDeviceStatusEntity()
            {
                DeviceName = DeviceName,
                RequestOn = isTurnOn,
                TypeOnOff = ((int)TypeOnOff.Threshold),
                ValueDate = DateTime.UtcNow,
                ThresholdId = thresholdId,
            });

            return IsComplete;
        }
    }
}
