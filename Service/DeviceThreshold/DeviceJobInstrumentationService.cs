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

        public async Task<bool> RunningJobThreshold()
        {
            loggerManager.LogInformation("Start running job threshold");
            var query = InstrumentationSetThresholdQuery.GetThresholdByInstrumentationId;

            IEnumerable<InstrumentationGetForSystem> result;
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                result = await connection.QueryAsync<InstrumentationGetForSystem>(query);
                connection.Close();
            }
            // thiết bị phải là tự động
            var dic = await dataStatisticsService.GetValueDeviceForThreshold(result.Where(p => p.AutoDevice));

            foreach (var entity in dic)
            {
                loggerManager.LogInformation("On/Off");
                await TurnOnOffDevice(entity.ModuleId, entity.DeviceId, entity.RequestOn, DeviceName: entity.DeviceNameNumber);
            }

            loggerManager.LogInformation("End running job threshold");
            return await Task.FromResult(true);
        }
        private async Task<bool> TurnOnOffDevice(Guid ModuleId, Guid DeviceId, bool isTurnOn, string? DeviceName)
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

            //logDeviceStatusEntities.Add(new LogDeviceStatusEntity()
            //{
            //    DeviceName = DeviceName,
            //    RequestOn = isTurnOn,
            //    TypeOnOff = ((int)TypeOnOff.Threshold),
            //    ValueDate = DateTime.UtcNow,
            //    ThresholdId = thresholdId,
            //});

            return IsComplete;
        }
    }
}
