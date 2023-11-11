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
        Task<List<DeviceDisplayModel>> DevicesDisplay(int zoneId);
        Task<DeviceModifyResponseModel> DeviceCreate(Guid deviceId, int zoneId);
        Task<DeviceModifyResponseModel> DeviceRemove(Guid deviceId, int zoneId);
    }
}
