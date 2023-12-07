using AutoMapper;
using Models;
using Models.Device;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class ModuleService : IModuleService
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

        public async Task<bool> AddModuleToUser(Guid moduleId, string userId, string nameRef)
        {
            return await _repositoryManager.Module.AddModuleToUser(moduleId, userId, nameRef);
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
        public async Task<List<ModuleDisplayModel>> GetModulesUsed(string userId)
        {
            var modules = await GetModules(userId); // Điều này giả sử bạn có một hàm GetModules để lấy danh sách các modules dựa trên userId.

            var result = modules
                .Select(module => new ModuleDisplayModel
                {
                    Id = module.Id,
                    Name = module.Name,
                    ModuleType = module.ModuleType,
                    DateCreated = module.DateCreated,
                    Note = module.Note,
                    MqttServer = module.MqttServer,
                    MqttPort = module.MqttPort,
                    ClientId = module.ClientId,
                    UserName = module.UserName,
                    Password = module.Password,
                    Devices = module.Devices?.Where(device => !device.IsUsed).ToList()
                })
                .ToList();

            return result;
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