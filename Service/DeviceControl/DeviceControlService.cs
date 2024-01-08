using Common.Queries;
using Dapper;
using Database;
using Models.DeviceControl;
using Models.DeviceTimer;
using MQTTProcess;
using Service.Contracts;
using Service.Contracts.Logger;

namespace Service
{
    public sealed class DeviceControlService : IDeviceControlService
    {
        private readonly DapperContext dapperContext;
        private readonly ILoggerManager logger;
        //private readonly IDeviceJobMqtt deviceJobMqtt;
        private readonly IProcessJobControlDevice processJobControlDevice;

        public DeviceControlService(DapperContext dapperContext, ILoggerManager logger, IProcessJobControlDevice processJobControlDevice)
        {
            this.dapperContext = dapperContext;
            this.logger = logger;
            this.processJobControlDevice = processJobControlDevice;
        }

        #region Open or Close Device

        // Tắt device driver ---> IsAcction = false || mở device IsAcction = true
        // Vd: Systemid/ModuleId/DeviceId/MB-1
        public async Task<bool> DeviceDriverOnOff(OnOffDeviceQueryModel model)
        {
            logger.LogInformation($"DeviceDriver: Turn off or on --> DeviceDriverId: {model.DeviceId}");

            string OnOffSQL = DeviceQuery.UpdateTurnOnOffSQL;
            //var turnOffByMqtt = await deviceJobMqtt.OnOffDevice(model);
            var turnOffByMqtt = await processJobControlDevice.OnOffDevice(model);
            int ChangeStageDB = 0;
            if (turnOffByMqtt)
            {
                var connection = dapperContext.CreateConnection();
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    ChangeStageDB = await connection.ExecuteAsync(OnOffSQL, new { Id = model.DeviceId, IsAction = (model.RequestOn) ? 1 : 0 }, transaction: trans);
                    trans.Commit();
                }
                connection.Close();
            }

            return ChangeStageDB > 0;
        }
        // Hàm này dùng để set cho trạng thái của IsRemove = true và IsSuccess = true
        public async Task<bool> SuccessJobTurnOnDeviceTimer(int timerId, Guid deviceId)
        {
            logger.LogInformation($"DeviceDriver: Set status to complete --> DeviceDriverId: {timerId}");
            var query = DeviceQuery.SuccessJobTurnOnDeviceTimerSQL;
            var connection = dapperContext.CreateConnection();
            connection.Open();
            int execute;
            using (var trans = connection.BeginTransaction())
            {
                execute = await connection.ExecuteAsync(query, new { Id = timerId, DeviceId = deviceId }, transaction: trans);
                trans.Commit();
            }
            connection.Close();
            return execute > 0;
        }
        public async Task<bool> SuccessJobTurnOffDeviceTimer(int timerId, Guid deviceId)
        {
            logger.LogInformation($"DeviceDriver: Set status to complete --> DeviceDriverId: {timerId}");
            var query = DeviceQuery.SuccessJobTurnOnDeviceTimerSQL;
            var connection = dapperContext.CreateConnection();
            connection.Open();
            int execute;
            using (var trans = connection.BeginTransaction())
            {
                execute = await connection.ExecuteAsync(query, new { Id = timerId, DeviceId = deviceId }, transaction: trans);
                trans.Commit();
            }
            connection.Close();
            return execute > 0;
        }

        public async Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerAvailable()
        {
            var query = DeviceQuery.GetAllTimerAvailable;
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<TimerDeviceDriverDisplayModel>(query);
                connection.Close();
                return result;
            }
        }
        #endregion


        #region Async status
        public async Task<bool> AsyncStatusDeviceControl()
        {
            var query = DeviceQuery.AsyncDeviceIsActionSQL;
            IEnumerable<StatusDeviceControlModel> result;
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                result = await connection.QueryAsync<StatusDeviceControlModel>(query);
                connection.Close();
            }
            var result1 = result.ToList();
            return await processJobControlDevice.AsyncStatusDeviceControl(result1);
        }
        #endregion
    }
}