using AutoMapper;
using Models.Device;
using Repository.Contracts;
using Service.Contracts.Device;

namespace Service.Device
{
    public class DeviceService : IDeviceService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        public DeviceService(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<DeviceModifyResponseModel> DeviceCreate(Guid deviceId, int zoneId)
        {
            await repository.device.DeviceCreate(deviceId, zoneId);
            var change = await repository.SaveAsync();
            var response = new DeviceModifyResponseModel();
            if (change > 0)
            {
                response.success = true;
                response.displayModels = await DevicesDisplay(zoneId);
            }
            else
            {
                response.success = false;
                response.displayModels = await DevicesDisplay(zoneId);
            }
            return response;
        }

        public async Task<DeviceModifyResponseModel> DeviceRemove(Guid deviceId, int zoneId)
        {
            await repository.device.DeviceRemove(deviceId, zoneId);
            var change = await repository.SaveAsync();
            var response = new DeviceModifyResponseModel();
            if (change > 0)
            {
                response.success = true;
                response.displayModels = await DevicesDisplay(zoneId);
            }
            else
            {
                response.success = false;
                response.displayModels = await DevicesDisplay(zoneId);
            }
            return response;
        }

        public async Task<List<DeviceDisplayModel>> DevicesDisplay(int zoneId)
        {
            var deviceEntities = await repository.device.DevicesDisplay(zoneId);
            var deviceModels = mapper.Map<List<DeviceDisplayModel>>(deviceEntities);
            return deviceModels;
        }
    }
}
