using Models.Device;

/// <summary>
/// Sử dụng trong page Zone
/// 
/// - Display Device -> Get device have ZoneId = param ZoneId, isUsed = true
/// - Thêm Device bằng cách ,DeviceId -> isUsed = true.
/// - Xoá Device bằng cách Chon DeviceId -> isUsed = false.
/// 
/// </summary>

namespace Service.Contracts.Device
{
    public interface IDeviceService
    {
        Task<List<DeviceDisplayModel>> GetDevicesOnZone(int zoneId);
        Task<List<DeviceDisplayModel>> GetDevicesControlOnZone(int zoneId);
        Task<List<DeviceDisplayModel>> GetDevicesInstrumentationOnZone(int zoneId);
        Task<bool> AddDeviceToZone(Guid deviceId, int zoneId);
        Task<bool> RemoveDeviceFromZone(Guid deviceId, int zoneId);
        Task<bool> SetAutoDevice(Guid deviceId, bool IsAuto);
    }
}
