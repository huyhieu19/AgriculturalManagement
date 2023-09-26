using Entities;
using Models;

namespace Repository.Contracts
{
    public interface IDeviceDriverTypeRepository
    {
        Task<bool> CreateTypeDeviceDrivers(List<DeviceDriversTypeDisplayModel> model);
        Task<bool> UpdateTypeDeviceDriver(DeviceDriversTypeDisplayModel model);
        Task<bool> DeleteTypeDeviceDrivers(List<int> Ids);
        Task<List<DeviceDriverTypeEntity>> GetTypeDeviceDrivers();
    }
}
