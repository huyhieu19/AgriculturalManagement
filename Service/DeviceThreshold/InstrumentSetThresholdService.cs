using AutoMapper;
using Common.Queries;
using Dapper;
using Database;
using Entities;
using Models;
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

        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOff(string userId)
        {
            var query = InstrumentationSetThresholdQuery.GetThreshold;
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

        public async Task DeviceInstrumentOnOffCreate(InstrumentSetThresholdCreateModel model)
        {
            var create = mapper.Map<ThresholdDeviceEntity>(model);
            repositoryManager.InstrumentSetThreshold.DeviceInstrumentOnOffCreate(create);
            await repositoryManager.SaveAsync();
        }

        public async Task DeviceInstrumentOnOffDeleteById(int Id)
        {
            repositoryManager.InstrumentSetThreshold.DeviceInstrumentOnOffDeleteById(Id);
            await repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffDelete()
        {
            var result = await repositoryManager.InstrumentSetThreshold.DeviceInstrumentOnOffDelete();
            return mapper.Map<IEnumerable<InstrumentSetThresholdDisplayModel>>(result);
        }

        //public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffNotDelete()
        //{
        //    var result = await repositoryManager.InstrumentSetThreshold.DeviceInstrumentOnOffNotDelete();
        //    return mapper.Map<IEnumerable<InstrumentSetThresholdDisplayModel>>(result);
        //}

        public async Task DeviceInstrumentOnOffUpdate(InstrumentSetThresholdUpdateModel updateModel)
        {
            var update = mapper.Map<ThresholdDeviceEntity>(updateModel);
            repositoryManager.InstrumentSetThreshold.DeviceInstrumentOnOffUpdate(update);
            await repositoryManager.SaveAsync();
        }
    }
}