﻿using AutoMapper;
using Common.Queries;
using Dapper;
using Database;
using Entities;
using Models;
using Models.InstrumentSetThreshold;
using Repository.Contracts;
using Service.Contracts.DeviceThreshold;
using Service.Contracts.Logger;

namespace Service.DeviceThreshold
{
    public sealed class InstrumentSetThresholdService : IInstrumentSetThresholdService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;
        private readonly DapperContext dapperContext;
        private readonly ILoggerManager logger;

        public InstrumentSetThresholdService(IRepositoryManager repositoryManager, IMapper mapper, DapperContext dapperContext, ILoggerManager logger)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
            this.dapperContext = dapperContext;
            this.logger = logger;
        }

        // sử dụng cho việc lấy dữ liệu hiển thị cho người dùng
        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOff(string userId)
        {
            var query = InstrumentationSetThresholdQuery.GetThresholdForUser;
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<InstrumentSetThresholdDisplayModel>(query, new { UserId = userId });
                connection.Close();
                return result;
            }
        }

        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffByIdDeviceDriver(Guid Id)
        {
            var result = await repositoryManager.InstrumentSetThreshold.DeviceInstrumentOnOffByIdDeviceDriver(Id);
            return mapper.Map<IEnumerable<InstrumentSetThresholdDisplayModel>>(result);
        }

        public async Task<bool> DeviceInstrumentOnOffCreate(InstrumentSetThresholdCreateModel model)
        {
            var create = mapper.Map<ThresholdDeviceEntity>(model);
            await repositoryManager.InstrumentSetThreshold.DeviceInstrumentOnOffCreate(create);
            return await repositoryManager.SaveAsync() > 0;
        }

        public async Task<bool> DeviceInstrumentOnOffDeleteById(ThresholdRemoveModel model)
        {
            return await repositoryManager.InstrumentSetThreshold.DeviceInstrumentOnOffDeleteById(model);
        }

        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffDelete()
        {
            var result = await repositoryManager.InstrumentSetThreshold.DeviceInstrumentOnOffDelete();
            return mapper.Map<IEnumerable<InstrumentSetThresholdDisplayModel>>(result);
        }

        public async Task<bool> DeviceInstrumentOnOffUpdate(InstrumentSetThresholdUpdateModel updateModel)
        {
            var update = mapper.Map<ThresholdDeviceEntity>(updateModel);
            await repositoryManager.InstrumentSetThreshold.DeviceInstrumentOnOffUpdate(update);
            return await repositoryManager.SaveAsync() > 0;
        }
    }
}