using AutoMapper;
using Entities.ESP;
using Models;
using Models.ESP;
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
            _repositoryManager.EspRepository.CreateEsp(entity);
            int isChange = await _repositoryManager.SaveAsync();
            return isChange == 1;
        }

        public async Task<bool> DeleteESP(Guid id)
        {
            _repositoryManager.EspRepository.DeleteEsp(id);
            int isChange = await _repositoryManager.SaveAsync();
            return isChange == 1;
        }

        public async Task<bool> AddEspToUser(Guid espId, string userId)
        {
            return await _repositoryManager.EspRepository.AddEspToUser(espId, userId);
        }

        public async Task<List<DeviceESPDisplayModel>> DeviceESPDisplay(Guid id)
        {
            var entity = await _repositoryManager.DeviceEspRepository.DeviceESPDisplay(id);
            return _mapper.Map<List<DeviceESPDisplayModel>>(entity);
        }

        public async Task<bool> DeviceESPCreate(DeviceESPCreateModel model)
        {
            var entity = _mapper.Map<DeviceTypeEspEntity>(model);



            _repositoryManager.DeviceEspRepository.DeviceESPCreate(entity);
            int change = await _repositoryManager.SaveAsync();
            return change == 1;
        }

        public async Task<bool> DeviceESPRemove(Guid id)
        {
            _repositoryManager.DeviceEspRepository.DeviceESPRemove(id);
            int change = await _repositoryManager.SaveAsync();
            return change == 1;
        }

        public async Task<List<EspDisplayModel>> GetEspsAll()
        {
            var entity = await _repositoryManager.EspRepository.GetEspsAll();
            return _mapper.Map<List<EspDisplayModel>>(entity);
        }

        public async Task<List<EspDisplayModel>> GetEsps(string id)
        {
            var entity = await _repositoryManager.EspRepository.GetEsps(id);
            return _mapper.Map<List<EspDisplayModel>>(entity);
        }

    }
}