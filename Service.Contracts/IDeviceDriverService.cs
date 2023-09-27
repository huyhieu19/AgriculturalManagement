using Models;
using Models.DeviceDriver;

namespace Service.Contracts
{
    public interface IDeviceDriverService
    {
        Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverByZoneAsync(int Id);
        Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDrivernNotInZoneAsync();
        Task UpdateInforDeviceDriver(DeviceDriverUpdateModel updateModel);
        Task CreateDeviceDrivern(DeviceDriverCreateModel createModel);
        Task DeleteDeviceDriver(int Id); //  Xóa hẳn => bị hỏng máy
        Task RemoveDeviceDriver(int Id); //chuyển Zone Id  = null
    }
}
