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
        private readonly IProcessJobControlDevice processJobControlDevice;

        public DeviceControlService(DapperContext dapperContext, ILoggerManager logger, IProcessJobControlDevice processJobControlDevice)
        {
            this.dapperContext = dapperContext;
            this.logger = logger;
            this.processJobControlDevice = processJobControlDevice;
        }
        #region Open or Close Device

        // Đóng/mở thiết bị ngoại vi
        public async Task<bool> DeviceDriverOnOff(OnOffDeviceQueryModel model)
        {
            try
            {
                logger.LogInformation($"DeviceDriver: Turn off or on --> DeviceDriverId: {model.DeviceId}");

                string OnOffSQL = DeviceQuery.UpdateTurnOnOffSQL;
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
            catch
            {
                throw;
            }
        }

        // Hàm này dùng để set cho trạng thái của IsRemove = true và IsSuccess = true
        public async Task<bool> SuccessJobTurnOnOffDeviceTimer(int timerId, Guid deviceId, bool IsTurnOn)
        {
            try
            {
                logger.LogInformation($"DeviceDriver: Set status to complete --> DeviceDriverId: {timerId}");
                if (IsTurnOn)
                {
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
                else
                {
                    var query = DeviceQuery.SuccessJobTurnOffDeviceTimerSQL;
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
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerAvailable()
        {
            try
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
            catch
            {
                throw;
            }
        }
        #endregion


        #region Async status
        public async Task<bool> AsyncStatusDeviceControl()
        {
            try
            {
                // lấy ra thiết bị ngoại vi và trạng thái của thiết bị
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
            catch
            {
                logger.LogInformation("Error async status");
                throw;
            }
        }
        #endregion
    }
}