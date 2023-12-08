// Ignore Spelling: Accessor

using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Administrator")]
        public async Task<List<ModuleDisplayModel>> GetModulesAll()
        {
            return await _serviceManager.Module.GetModulesAll();
        }

        [HttpPost("get-modules")]
        [Authorize(Roles = "Administrator")]
        public async Task<List<ModuleDisplayModel>> GetModules()
        {
            var userId = _contextAccessor.HttpContext!.User.FindFirst("Id")!.Value;
            return await _serviceManager.Module.GetModules(userId);
        }
        [HttpPost("get-modules-devices-used")]
        [Authorize(Roles = "Administrator")]
        public async Task<List<ModuleDisplayModel>> GetModulesUsed()
        {
            var userId = _contextAccessor.HttpContext!.User.FindFirst("Id")!.Value;
            return await _serviceManager.Module.GetModulesUsed(userId);
        }

        [HttpPost("add-module-to-user")]
        [Authorize(Roles = "Administrator")]
        public async Task<bool> AddModuleToUser(ModuleAddModel model)
        {
            var userId = _contextAccessor.HttpContext!.User.FindFirst("Id")!.Value;
            Guid moduleId = model.ModuleId;
            string nameRef = model.NameRef!;
            return await _serviceManager.Module.AddModuleToUser(moduleId, userId, nameRef);
        }

        [HttpPost("edit-module")]
        [Authorize(Roles = "Administrator")]
        public async Task<bool> EditModule(ModuleUpdateModel model)
        {
            return await _serviceManager.Module.EditModule(model);
        }

        [HttpPost("remove-module-from-user")]
        [Authorize(Roles = "Administrator")]
        public async Task<bool> RemoveModuleFromUser(Guid moduleId)
        {

            var userId = _contextAccessor.HttpContext!.User.FindFirst("Id")!.Value;
            return await _serviceManager.Module.RemoveModuleFromUser(moduleId, userId);
        }
        #endregion

        #region Device on module - Get & Remove
        [HttpPost("devices-on-module")]
        [Authorize(Roles = "Administrator")]
        public async Task<List<DeviceDisplayModel>> DeviceOnModuleDisplay(Guid moduleId)
        {
            return await _serviceManager.Module.DeviceOnModuleDisplay(moduleId);
        }

        [HttpPost("edit-devices")]
        [Authorize(Roles = "Administrator")]
        public async Task<bool> UpdateDevice(DeviceEditModel devices)
        {
            return await _serviceManager.Module.UpdateDevice(devices);
        }

        [HttpPost("devices-used-on-module")]
        [Authorize(Roles = "Administrator")]
        public async Task<List<DeviceDisplayModel>> DeviceUsedOnModuleDisplay(Guid moduleId)
        {
            return await _serviceManager.Module.DeviceUsedOnModuleDisplay(moduleId);
        }

        [HttpPost("devices-not-used-on-module")]
        [Authorize(Roles = "Administrator")]
        public async Task<List<DeviceDisplayModel>> DeviceUsedNotOnModuleDisplay(Guid moduleId)
        {
            return await _serviceManager.Module.DeviceUsedNotOnModuleDisplay(moduleId);
        }
        #endregion
    }
}