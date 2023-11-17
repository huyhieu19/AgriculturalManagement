using AutoMapper;
using Common.Queries;
using Dapper;
using Database;
using Entities;
using Models;
using Models.DeviceAuto;
using Models.DeviceControl;
using Models.DeviceTimer;
using MQTTProcess;
using Service.Contracts;
using Service.Contracts.Logger;

namespace Service
{
    public class DeviceControlService : IDeviceControlService
    {
        private readonly IMapper mapper;
        private readonly DapperContext dapperContext;
        private readonly ILoggerManager logger;
        private readonly IDeviceJobMqtt deviceJobMqtt;

        public DeviceControlService(IMapper mapper, DapperContext dapperContext, ILoggerManager logger, IDeviceJobMqtt deviceJobMqtt)
        {
            this.mapper = mapper;
            this.dapperContext = dapperContext;
            this.logger = logger;
            this.deviceJobMqtt = deviceJobMqtt;
        }

        #region Open or Close Device

        // Tắt device driver ---> IsAcction = false || mở device IsAcction = true
        // Vd: Systemid/a0722586-845e-11ee-b962-0242ac120002/W/MB-1
        public async Task<bool> DeviceDriverOnOff(OnOffDeviceQueryModel model)
        {
            logger.LogInformation($"DeviceDriver: Turn off or on --> DeviceDriverId: {model.DeviceId}");

            string TurnOnSQL = DeviceQuery.UpdateTurnOnSQL;
            string TurnOffSQL = DeviceQuery.UpdateTurnOffSQL;
            var turnOffByMqtt = await deviceJobMqtt.OnOffDevice(model);
            int ChangeStageDB = 0;
            if (turnOffByMqtt && model.RequestOn)
            {
                var connection = dapperContext.CreateConnection();
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    ChangeStageDB = await connection.ExecuteAsync(TurnOffSQL, new { Id = model.DeviceId }, transaction: trans);
                    trans.Commit();
                }
                connection.Close();
            }
            else if(turnOffByMqtt && !model.RequestOn)
            {
                var connection = dapperContext.CreateConnection();
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    ChangeStageDB = await connection.ExecuteAsync(TurnOnSQL, new { Id = model.DeviceId }, transaction: trans);
                    trans.Commit();
                }
                connection.Close();
            }
            return ChangeStageDB > 0;
        }
        // Hàm này dùng để set cho trạng thái của IsRemve = true và IsSuccess = true
        public async Task<bool> SuccessJobTimer(int timerId, Guid deviceId)
        {
            logger.LogInformation($"DeviceDriver: Set status to complete --> DeviceDriverId: {timerId}");
            var query = DeviceQuery.SuccessTimerSQL;
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
    }
}