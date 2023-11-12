using Models;
using Models.Device;

namespace Service.Contracts
{
    /*
     * -- Screen: Manager Module Device
     */

    public interface IModuleService
    {
        #region Module
        Task<List<ModuleDisplayModel>> GetModulesAll();
        Task<List<ModuleDisplayModel>> GetModules(string userId);
        Task<bool> RemoveModuleFromUser(Guid moduleId, string userId);
        Task<bool> AddModuleToUser(Guid moduleId, string userId);
        #endregion

        #region Device on module
        Task<List<DeviceDisplayModel>> DeviceOnModuleDisplay(Guid moduleId);
        Task<List<DeviceDisplayModel>> DeviceUsedOnModuleDisplay(Guid moduleId);
        Task<List<DeviceDisplayModel>> DeviceUsedNotOnModuleDisplay(Guid moduleId);
        #endregion
    }
}