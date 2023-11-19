using AutoMapper;
using Models;
using Models.Device;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public class ModuleService : IModuleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ModuleService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        #region Module
        public async Task<bool> RemoveModuleFromUser(Guid moduleId, string userId)
        {
            await _repositoryManager.Module.RemoveModuleFromUser(moduleId, userId);
            int isChange = await _repositoryManager.SaveAsync();
            return isChange == 1;
        }

        public async Task<bool> AddModuleToUser(Guid moduleId, string userId)
        {
            return await _repositoryManager.Module.AddModuleToUser(moduleId, userId);
        }

        public async Task<List<ModuleDisplayModel>> GetModulesAll()
        {
            var entity = await _repositoryManager.Module.GetModulesAll();
            return _mapper.Map<List<ModuleDisplayModel>>(entity);
        }

        public async Task<List<ModuleDisplayModel>> GetModules(string userId)
        {
            var entity = await _repositoryManager.Module.GetModules(userId);
            return _mapper.Map<List<ModuleDisplayModel>>(entity);
        }
        #endregion

        #region Device On Module
        public async Task<List<DeviceDisplayModel>> DeviceOnModuleDisplay(Guid moduleId)
        {
            var entity = await _repositoryManager.Device.DeviceOnModuleDisplay(moduleId);
            return _mapper.Map<List<DeviceDisplayModel>>(entity);
        }

        public async Task<List<DeviceDisplayModel>> DeviceUsedOnModuleDisplay(Guid moduleId)
        {
            var devices = await DeviceOnModuleDisplay(moduleId);
            return devices.Where(p => p.IsUsed == true && p.ZoneId != null).ToList();
        }

        public async Task<List<DeviceDisplayModel>> DeviceUsedNotOnModuleDisplay(Guid moduleId)
        {
            var devices = await DeviceOnModuleDisplay(moduleId);
            return devices.Where(p => p.IsUsed == false && p.ZoneId == null).ToList();
        }

        #endregion
    }
}