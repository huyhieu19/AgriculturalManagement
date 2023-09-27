using AutoMapper;
using Entities;
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

        public async Task CreateDeviceDriver(DeviceDriverCreateModel createModel)
        {
            DeviceDriverEntity entity = mapper.Map<DeviceDriverEntity>(createModel);
            repositoryManager.DeviceDriver.CreateDeviceDriver(entity);
            await repositoryManager.SaveAsync();
        }

        public async Task DeleteDeviceDriver(int Id)
        {

            repositoryManager.DeviceDriver.DeleteDeviceDriver(new DeviceDriverEntity() { Id = Id });
            await repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverByZoneAsync(int Id)
        {
            return await repositoryManager.DeviceDriver.GetDeviceDriverByZoneAsync(Id);
        }

        public async Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverNotInZoneAsync()
        {
            IEnumerable<DeviceDriverEntity> models = await repositoryManager.DeviceDriver.GetDeviceDriverNotInZoneAsync();
            return mapper.Map<IEnumerable<DeviceDriverDisplayModel>>(models);
        }

        public async Task RemoveDeviceDriver(int Id)
        {
            await repositoryManager.DeviceDriver.RemoveDeviceDriver(Id);
            await repositoryManager.SaveAsync();
        }

        public async Task UpdateInforDeviceDriver(DeviceDriverUpdateModel updateModel)
        {
            DeviceDriverEntity entity = mapper.Map<DeviceDriverEntity>(updateModel);
            repositoryManager.DeviceDriver.UpdateInforDeviceDriver(entity);
            await repositoryManager.SaveAsync();
        }
    }
}
