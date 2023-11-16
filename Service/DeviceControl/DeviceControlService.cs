using AutoMapper;
using Common.Queries;
using Dapper;
using Database;
using Entities;
using Models;
using Models.DeviceAuto;
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

        #region Device
        // Hàm này dùng để mở device driver ---> IsAcction = true
        public async Task DeviceDriverTurnOn(Guid DeviceDriverId)
        {
            logger.LogInformation($"DeviceDriver: Turn on --> DeviceDriverId:  {DeviceDriverId}");
            var connection = dapperContext.CreateConnection();
            connection.Open();
            using (var trans = connection.BeginTransaction())
            {
                await connection.ExecuteAsync(TimerDeviceDriverQuery.UpdateTurnOnSQL, new { Id = DeviceDriverId }, transaction: trans);
                trans.Commit();
            }
            connection.Close();
        }

        // Hàm này dùng để tắt device driver ---> IsAcction = false
        // a0722586-845e-11ee-b962-0242ac120002
        public async Task<bool> DeviceDriverTurnOff(Guid DeviceDriverId)
        {
            logger.LogInformation($"DeviceDriver: Turn off --> DeviceDriverId: {DeviceDriverId}");
            //var connection = dapperContext.CreateConnection();
            //connection.Open();
            //using (var trans = connection.BeginTransaction())
            //{
            //    await connection.ExecuteAsync(TimerDeviceDriverQuery.UpdateTurnOffSQL, new { Id = DeviceDriverId }, transaction: trans);
            //    trans.Commit();
            //}
            //connection.Close();
            return await deviceJobMqtt.TurnOffDevice(new Guid($"{DeviceDriverId}"), "W", "MB-1");
        }

        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffNotDelete()
        {
            var query = InstrumentationSetThresholdQuery.InstrumentationNotDelete;
            IEnumerable<ThresholdDeviceEntity> result;
            using (var connection = dapperContext.CreateConnection())
            {
                result = await connection.QueryAsync<ThresholdDeviceEntity>(query);
            }
            return mapper.Map<IEnumerable<InstrumentSetThresholdDisplayModel>>(result);
        }
        #endregion
    }
}