using Entities.Module;
using Models;

namespace Repository.Contracts
{
    public interface IModuleRepository
    {
        Task<List<ModuleEntity>> GetModules(string userId);
        Task RemoveModuleFromUser(Guid moduleId, string userId);
        Task<bool> EditModule(ModuleUpdateModel model);
        Task<bool> AddModuleToUser(Guid moduleId, string userId, string nameRef);
        Task<List<ModuleEntity>> GetModulesAll();
    }
}