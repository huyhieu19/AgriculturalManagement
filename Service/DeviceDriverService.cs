using AutoMapper;
using Models;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public class DeviceDriverService : IDeviceDriverService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public DeviceDriverService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        public Task CreateInstrumentation(InstrumentationCreateModel createModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteInstrumentation(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InstrumentationDisplayModel>> GetDeviceDriverByZoneAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationNotInZoneAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveInstrumentation(int Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInforInstrumentation(InstrumentationUpdateModel updateModel)
        {
            throw new NotImplementedException();
        }
    }
}
