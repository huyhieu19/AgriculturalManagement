using Entities;

namespace Repository.Contracts
{
    public interface IDeviceDriverTypeRepository
    {
        void CreateTypeDeviceDrivers(DeviceDriverTypeEntity entity);
        void UpdateTypeDeviceDriver(DeviceDriverTypeEntity entity);
        void DeleteTypeDeviceDrivers(DeviceDriverTypeEntity entity);
        Task<List<DeviceDriverTypeEntity>> GetTypeDeviceDrivers();
    }
}
