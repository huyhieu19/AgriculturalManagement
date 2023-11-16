using AutoMapper;
using Entities.ESP;
using Models;
using Models.Device;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public class EspService : IEspService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public EspService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<bool> CreateEsp(EspCreateModel model)
        {
            var entity = _mapper.Map<EspEntity>(model);
            _repositoryManager.Esp.CreateEsp(entity);
            int isChange = await _repositoryManager.SaveAsync();
            return isChange == 1;
        }

        public async Task<bool> DeleteESP(Guid id)
        {
            _repositoryManager.Esp.DeleteEsp(id);
            int isChange = await _repositoryManager.SaveAsync();
            return isChange == 1;
        }

        public async Task<bool> AddEspToUser(Guid espId, string userId)
        {
            return await _repositoryManager.Esp.AddEspToUser(espId, userId);
        }

        public async Task<List<DeviceDisplayModel>> DeviceESPDisplay(Guid id)
        {
            var entity = await _repositoryManager.DeviceEsp.DeviceESPDisplay(id);
            return _mapper.Map<List<DeviceDisplayModel>>(entity);
        }

        public async Task<bool> DeviceESPCreate(DeviceCreateModel model)
        {
            var entity = _mapper.Map<DeviceEntity>(model);



            _repositoryManager.DeviceEsp.DeviceESPCreate(entity);
            int change = await _repositoryManager.SaveAsync();
            return change == 1;
        }

        public async Task<bool> DeviceESPRemove(Guid id)
        {
            _repositoryManager.DeviceEsp.DeviceESPRemove(id);
            int change = await _repositoryManager.SaveAsync();
            return change == 1;
        }

        public async Task<List<EspDisplayModel>> GetEspsAll()
        {
            var entity = await _repositoryManager.Esp.GetEspsAll();
            return _mapper.Map<List<EspDisplayModel>>(entity);
        }

        public async Task<List<EspDisplayModel>> GetEsps(string id)
        {
            var entity = await _repositoryManager.Esp.GetEsps(id);
            return _mapper.Map<List<EspDisplayModel>>(entity);
        }

    }
}