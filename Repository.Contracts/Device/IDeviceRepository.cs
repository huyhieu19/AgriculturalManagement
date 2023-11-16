using Entities.ESP;

namespace Repository.Contracts.Device
{
    /// <summary>
    /// Sử dụng trong page Zone
    /// 
    /// - Display Device -> Get device have ZoneId = param ZoneId, isUsed = true
    /// - Thêm Device bằng cách ,DeviceId -> isUsed = true.
    /// - Xoá Device bằng cách Chon DeviceId -> isUsed = false.
    /// 
    /// </summary>
    public interface IDeviceRepository
    {
        Task<List<DeviceEntity>> DevicesDisplay(int zoneId);
        Task DeviceCreate(Guid deviceId, int ZoneId);
        Task DeviceRemove(Guid deviceId, int ZoneId);
    }
}
