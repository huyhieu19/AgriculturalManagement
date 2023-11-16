using Common.Enum;
using Entities.Module;

namespace Repository.Contracts
{
    public interface IMockDataRepository
    {
        Task<bool> MockModule(ModuleEntity entity);
        Task<bool> MockDevicesOnModule(List<DeviceEntity> devices);
        Task<ModuleType> GetTypeModule(Guid moduleId);
        Task<bool> DeleteMockDataDeviceOnModule(Guid moduleId);
    }
}
