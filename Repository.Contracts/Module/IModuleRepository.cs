using Entities.Module;

namespace Repository.Contracts
{
    public interface IModuleRepository
    {
        Task<List<ModuleEntity>> GetModules(string id);
        Task RemoveModuleFromUser(Guid moduleId, string userId);
        Task<bool> AddModuleToUser(Guid espId, string userId);
        Task<List<ModuleEntity>> GetModulesAll();
    }
}