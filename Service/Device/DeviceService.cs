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

        public async Task<bool> AddDeviceToZone(Guid deviceId, int zoneId)
        {
            await repository.Device.AddDeviceToZone(deviceId, zoneId);
            return await repository.SaveAsync() > 0;
        }

        public async Task<bool> RemoveDeviceFromZone(Guid deviceId, int zoneId)
        {
            await repository.Device.RemoveDeviceFromZone(deviceId, zoneId);
            return await repository.SaveAsync() > 0;
        }

        private async Task<List<DeviceDisplayModel>> GetDevicesOnZone(int zoneId)
        {
            var deviceEntities = await repository.Device.GetDevicesOnZone(zoneId);
            var deviceModels = mapper.Map<List<DeviceDisplayModel>>(deviceEntities);
            return deviceModels;
        }

        public async Task<List<DeviceDisplayModel>> GetDevicesControlOnZone(int zoneId)
        {
            var devices = await GetDevicesOnZone(zoneId);
            var result = devices.Where(prop => prop.DeviceType == Common.Enum.DeviceType.W.ToString()).ToList();
            return result;
        }

        public async Task<List<DeviceDisplayModel>> GetDevicesInstrumentationOnZone(int zoneId)
        {
            var devices = await GetDevicesOnZone(zoneId);
            var result = devices.Where(prop => prop.DeviceType == Common.Enum.DeviceType.R.ToString()).ToList();
            return result;
        }
    }
}
