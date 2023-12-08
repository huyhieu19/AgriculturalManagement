using Entities.Module;
using Models;

namespace Repository.Contracts.Device
{

    public interface IDeviceRepository
    {
        #region Screen Device On Zone Management
        Task<List<DeviceEntity>> GetDevicesOnZone(int zoneId);
        Task AddDeviceToZone(Guid deviceId, int ZoneId);
        Task RemoveDeviceFromZone(Guid deviceId, int ZoneId);
        #endregion

        #region Screen Device On Module Device Management
        Task<List<DeviceEntity>> DeviceOnModuleDisplay(Guid moduleId);
        Task<bool> UpdateDevice(DeviceEditModel device);
        #endregion
    }
}
