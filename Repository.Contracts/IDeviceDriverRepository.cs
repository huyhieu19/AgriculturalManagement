using Entities;
using Models;

namespace Repository.Contracts
{
    public interface IDeviceDriverRepository
    {
        Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverByZoneAsync(int Id);
        Task<IEnumerable<DeviceDriverEntity>> GetDeviceDriverNotInZoneAsync();
        void UpdateInforDeviceDriver(DeviceDriverEntity updateModel);
        void CreateDeviceDriver(DeviceDriverEntity createModel);
        void DeleteDeviceDriver(DeviceDriverEntity Id); //  Xóa hẳn => bị hỏng máy
        Task RemoveDeviceDriver(int Id); //chuyển Zone Id  = null

        Task<IEnumerable<DeviceDriverEntity>> GetDeviceDriver();

        // timer
        void CreateTimer(TimerDeviceDriverEntity entity);
        void UpdateTimer(TimerDeviceDriverEntity entity);
    }
}
