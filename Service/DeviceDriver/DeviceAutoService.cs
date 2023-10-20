﻿using AutoMapper;
using Common.Queries;
using Dapper;
using Database;
using Entities;
using Models;
using Service.Contracts;

namespace Service
{
    public class DeviceAutoService : IDeviceAutoService
    {
        private readonly IMapper mapper;
        private readonly DapperContext dapperContext;
        private readonly ILoggerManager logger;

        public DeviceAutoService(IMapper mapper, DapperContext dapperContext, ILoggerManager logger)
        {
            this.mapper = mapper;
            this.dapperContext = dapperContext;
            this.logger = logger;
        }



        #region Timer
        // Hàm này dùng để set cho trạng thái của IsRemve = true và IsSuccess = true
        public async Task DeleteTimer(int id)
        {
            logger.LogInfomation($"DeviceDriver: Set status to complete --> DeviceDriverId: {id}");
            var query = TimerDeviceDriverQuery.RemoveTimerSQL;
            var connection = dapperContext.CreateConnection();
            connection.Open();
            using (var trans = connection.BeginTransaction())
            {
                await connection.ExecuteAsync(query, new { Id = id }, transaction: trans);
                trans.Commit();
            }
            connection.Close();
        }
        #endregion

        #region Device
        // Hàm này dùng để mở device driver ---> IsAcction = true
        public async Task DeviceDriverTurnOn(int DeviceDriverId)
        {
            logger.LogInfomation($"DeviceDriver: Turn on --> DeviceDriverId:  {DeviceDriverId}");
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
        public async Task DeviceDriverTurnOff(int DeviceDriverId)
        {
            logger.LogInfomation($"DeviceDriver: Turn off --> DeviceDriverId: {DeviceDriverId}");
            var connection = dapperContext.CreateConnection();
            connection.Open();
            using (var trans = connection.BeginTransaction())
            {
                await connection.ExecuteAsync(TimerDeviceDriverQuery.UpdateTurnOffSQL, new { Id = DeviceDriverId }, transaction: trans);
                trans.Commit();
            }
            connection.Close();
        }

        // Lấy giá trị list giá trị ngày giờ đóng mở và các giá trị ngưỡng
        public async Task<IEnumerable<DeviceDriverTurnOnTurnOffModel>> GetDeviceDriverTurnOnTurnOffModels()
        {
            logger.LogInfomation("Job background device driver --> start");
            var query = TimerDeviceDriverQuery.GetAllTimerSQL;
            IEnumerable<DeviceDriverTurnOnTurnOffModel> listTime;
            using (var connection = dapperContext.CreateConnection())
            {
                listTime = await connection.QueryAsync<DeviceDriverTurnOnTurnOffModel>(query);
            }
            logger.LogInfomation("Job background device driver --> end");
            return listTime;
        }

        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffNotDelete()
        {
            var query = InstrumentationSetThresholdQuery.InstrumentationNotDelete;
            IEnumerable<DeviceInstrumentThresholdEntity> result;
            using (var connection = dapperContext.CreateConnection())
            {
                result = await connection.QueryAsync<DeviceInstrumentThresholdEntity>(query);
            }
            return mapper.Map<IEnumerable<InstrumentSetThresholdDisplayModel>>(result);
        }
        #endregion
    }
}