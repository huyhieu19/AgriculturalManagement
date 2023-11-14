using Common.Enum;

namespace Service.Contracts
{
    public interface IMockDataService
    {
        Task<bool> MockDataModule(ModuleType type);
        Task<bool> MockDataDeviceOnModule(Guid moduleId);
        Task<bool> DeleteMockDataDeviceOnModule(Guid moduleId);
    }
}
