using Common.Queries;
using Dapper;
using Database;
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

        public DeviceJobInstrumentationService(DapperContext dapperContext,
            ILoggerManager loggerManager,
            IDeviceControlService deviceControlService)
        {
            this.dapperContext = dapperContext;
            this.loggerManager = loggerManager;
            this.deviceControlService = deviceControlService;
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

            if (result.Any() && successT)
            {
                foreach (var item in result)
                {
                    if (item.OnInUpperThreshold)
                    {
                        if (item.ThresholdValueOn < value && !item.DeviceDriverAction)
                        {
                            // Logic mở thiết bị điều khiển
                            return await TurnOnOffDevice(item.ModuleDeviceDrId, item.DeviceDriverId, true);
                        }
                        else if (item.ThresholdValueOff > value && item.DeviceDriverAction)
                        {
                            // Logic đóng thiết bị điều khiển
                            return await TurnOnOffDevice(item.ModuleDeviceDrId, item.DeviceDriverId, false);
                        }
                    }
                    else
                    {
                        if (item.ThresholdValueOn < value && item.DeviceDriverAction)
                        {
                            // Logic đóng thiết bị điều khiển 
                            return await TurnOnOffDevice(item.ModuleDeviceDrId, item.DeviceDriverId, false);
                        }
                        else if (item.ThresholdValueOff > value && !item.DeviceDriverAction)
                        {
                            // Logic mở thiết bị điều khiển
                            return await TurnOnOffDevice(item.ModuleDeviceDrId, item.DeviceDriverId, true);
                        }
                    }
                }
            }
            loggerManager.LogInformation("End running job threshold");
            return await Task.FromResult(false);
        }
        private async Task<bool> TurnOnOffDevice(Guid ModuleId, Guid DeviceId, bool isTurnOn)
        {
            loggerManager.LogInformation($"Off Device {DeviceId}");
            var model = new OnOffDeviceQueryModel()
            {
                ModuleId = ModuleId,
                DeviceId = DeviceId,
                RequestOn = isTurnOn,
            };
            var IsComplete = await deviceControlService.DeviceDriverOnOff(model);
            return IsComplete;
        }
    }
}
