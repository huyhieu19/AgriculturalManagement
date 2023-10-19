using AutoMapper;
using Entities;
using Models;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public class InstrumentSetThresholdService : IInstrumentSetThresholdService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public InstrumentSetThresholdService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOff()
        {
            var result = await repositoryManager.InstrumentSetThreshold.DeviceInstrumentOnOff();
            return mapper.Map<IEnumerable<InstrumentSetThresholdDisplayModel>>(result);
        }

        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffByIdDeviceDriver(int Id)
        {
            var result = await repositoryManager.InstrumentSetThreshold.DeviceInstrumentOnOffByIdDeviceDriver(Id);
            return mapper.Map<IEnumerable<InstrumentSetThresholdDisplayModel>>(result);
        }

        public async Task DeviceInstrumentOnOffCreate(InstrumentSetThresholdCreateModel model)
        {
            var create = mapper.Map<DeviceInstrumentThresholdEntity>(model);
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

        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffNotDelete()
        {
            var result = await repositoryManager.InstrumentSetThreshold.DeviceInstrumentOnOffNotDelete();
            return mapper.Map<IEnumerable<InstrumentSetThresholdDisplayModel>>(result);
        }

        public async Task DeviceInstrumentOnOffUpdate(InstrumentSetThresholdUpdateModel updateModel)
        {
            var update = mapper.Map<DeviceInstrumentThresholdEntity>(updateModel);
            repositoryManager.InstrumentSetThreshold.DeviceInstrumentOnOffUpdate(update);
            await repositoryManager.SaveAsync();
        }
    }
}
