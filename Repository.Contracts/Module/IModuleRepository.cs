using Entities.Module;

namespace Repository.Contracts
{
    public interface IModuleRepository
    {
        Task<List<ModuleEntity>> GetModules(string userId);
        Task RemoveModuleFromUser(Guid moduleId, string userId);
        Task<bool> AddModuleToUser(Guid moduleId, string userId);
        Task<List<ModuleEntity>> GetModulesAll();
    }
}