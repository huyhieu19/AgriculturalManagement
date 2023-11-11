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
        private async Task<DeviceModifyResponseModel> ResponseModel(int change, int zoneId)
        {
            var response = new DeviceModifyResponseModel()
            {
                deviceDisplays = await DevicesDisplay(zoneId)
            };
            if (change > 0)
            {
                response.isSuccess = true;
            }
            else
            {
                response.isSuccess = false;
            }
            return response;
        }

        public async Task<DeviceModifyResponseModel> DeviceCreate(Guid deviceId, int zoneId)
        {
            await repository.device.DeviceCreate(deviceId, zoneId);
            var change = await repository.SaveAsync();
            var response = await ResponseModel(change, zoneId);
            return response;
        }

        public async Task<DeviceModifyResponseModel> DeviceRemove(Guid deviceId, int zoneId)
        {
            await repository.device.DeviceRemove(deviceId, zoneId);
            var change = await repository.SaveAsync();
            var response = await ResponseModel(change, zoneId);
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
