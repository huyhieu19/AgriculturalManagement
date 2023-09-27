using AutoMapper;
using Models;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class DeviceDriverService : IDeviceDriverService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public DeviceDriverService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        public async Task CreateDeviceDrivern(DeviceDriverCreateModel createModel)
        {
            await repositoryManager.DeviceDriver.CreateDeviceDriver(createModel);
        }

        public async Task DeleteDeviceDriver(int Id)
        {
            await repositoryManager.DeviceDriver.DeleteDeviceDriver(Id);
        }

        public async Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverByZoneAsync(int Id)
        {
            return await repositoryManager.DeviceDriver.GetDeviceDriverByZoneAsync(Id);
        }

        public async Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverNotInZoneAsync()
        {
            return await repositoryManager.DeviceDriver.GetDeviceDriverNotInZoneAsync();
        }

        public async Task RemoveDeviceDriver(int Id)
        {
            await repositoryManager.DeviceDriver.RemoveDeviceDriver(Id);
        }

        public async Task UpdateInforDeviceDriver(DeviceDriverUpdateModel updateModel)
        {
            await repositoryManager.DeviceDriver.UpdateInforDeviceDriver(updateModel);
        }
    }
}
