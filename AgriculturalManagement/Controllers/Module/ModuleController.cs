// Ignore Spelling: Accessor

using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Device;
using Service.Contracts;

/*
 * -- This is the controller for device management screen --
 * 
 * -Includes functions:
 * + Get all modules available in database(not used and used).
 * + Get all Modules available in the database that are being used by user.
 * + Add module for a user
 * + Remove module for a user
 */
namespace AgriculturalManagement.Controllers.Module
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IHttpContextAccessor _contextAccessor;
        public ModuleController(IServiceManager serviceManager,
            IHttpContextAccessor contextAccessor)
        {
            _serviceManager = serviceManager;
            _contextAccessor = contextAccessor;
        }
        #region Module - get & add & remove
        [HttpPost("get-modules-all")]
        public async Task<List<ModuleDisplayModel>> GetModulesAll()
        {
            return await _serviceManager.Module.GetModulesAll();
        }

        [HttpPost("get-modules")]
        public async Task<List<ModuleDisplayModel>> GetModules()
        {
            var userId = _contextAccessor.HttpContext!.User.FindFirst("Id")!.Value;
            return await _serviceManager.Module.GetModules(userId);
        }

        [HttpPost("add-module-to-user")]
        public async Task<bool> AddModuleToUser(Guid moduleId)
        {
            var userId = _contextAccessor.HttpContext!.User.FindFirst("Id")!.Value;
            return await _serviceManager.Module.AddModuleToUser(moduleId, userId);

        }

        [HttpPost("remove-module-from-user")]
        public async Task<bool> RemoveModuleFromUser(Guid moduleId)
        {

            var userId = _contextAccessor.HttpContext!.User.FindFirst("Id")!.Value;
            return await _serviceManager.Module.RemoveModuleFromUser(moduleId, userId);
        }
        #endregion

        #region Device on module - Get & Remove
        [HttpPost("devices-on-module")]
        public async Task<List<DeviceDisplayModel>> DeviceOnModuleDisplay(Guid moduleId)
        {
            return await _serviceManager.Module.DeviceOnModuleDisplay(moduleId);
        }

        [HttpPost("devices-used-on-module")]
        public async Task<List<DeviceDisplayModel>> DeviceUsedOnModuleDisplay(Guid moduleId)
        {
            return await _serviceManager.Module.DeviceUsedOnModuleDisplay(moduleId);
        }

        [HttpPost("devices-not-used-on-module")]
        public async Task<List<DeviceDisplayModel>> DeviceUsedNotOnModuleDisplay(Guid moduleId)
        {
            return await _serviceManager.Module.DeviceUsedNotOnModuleDisplay(moduleId);
        }
        #endregion
    }
}