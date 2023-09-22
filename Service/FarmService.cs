using AutoMapper;
using Entities;
using Models;
using Models.Farm;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class FarmService : IFarmService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public FarmService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<bool> AddFarm(FarmCreateModel createModel)
        {
            _logger.LogInfomation("Create farm in Farm service layer");
            var companyEntity = _mapper.Map<FarmEntity>(createModel);
            repositoryManager.FarmRepository.CreateFarm(companyEntity);
            return Task.FromResult(true);
        }

        public async Task<IEnumerable<FarmDisplayModel>> GetAllFarmAsync(bool trackChanges)
        {
            _logger.LogInfomation("Get all farms in Service layer");
            var farms = await repositoryManager.FarmRepository.GetAllFarm(trackChanges);
            var farmsDisplayModel = _mapper.Map<IEnumerable<FarmDisplayModel>>(farms);
            return farmsDisplayModel;
        }

        public async Task<IEnumerable<FarmDisplayModel>> GetByCondition(QueryBaseModel model, bool trackChanges)
        {
            try
            {
                _logger.LogInfomation($"Farm Service| Get By Condition : {model.SearchTerm} {model.typeOrderBy} | start ");
                var responseEntities = await repositoryManager.FarmRepository.GetByCondition(model, trackChanges);
                var response = _mapper.Map<IEnumerable<FarmDisplayModel>>(responseEntities);
                _logger.LogInfomation($"Farm Service | Get By Condition | end ");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Farm Service | Delete | Exeption: {ex}");
                throw;
            }
        }

        public Task<bool> RemoveFarm(int id)
        {
            try
            {
                _logger.LogInfomation($"Farm Service | Remove Farm: {id}");
                repositoryManager.FarmRepository.DeleteFarm(id);
                return Task.FromResult(true);
            }catch (Exception ex)
            {
                _logger.LogError($"Farm Service | Exception: {ex}");
                return Task.FromResult(false);
            }
        }
        public Task<bool> UpdateFarm(FarmUpdateModel updateModel)
        {
            try
            {
                _logger.LogInfomation($"Farm Service | Udpate Farm: {updateModel}");
                FarmEntity farmEntity = _mapper.Map<FarmEntity>(updateModel); 
                repositoryManager.FarmRepository.UpdateFarm(farmEntity);
                return Task.FromResult(true);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Farm Service | Exception: {ex}");
                return Task.FromResult(false);
            }
        }
    }
}
