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

        public async Task<bool> AddFarm(FarmCreateModel createModel)
        {
            _logger.LogInfomation("Create farm in Farm service layer");
            var companyEntity = _mapper.Map<FarmEntity>(createModel);
            repositoryManager.FarmRepository.CreateFarm(companyEntity);
            await repositoryManager.SaveAsync();
            return true;
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
                _logger.LogInfomation($"Farm Service| Get By Condition | start ");
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

        public async Task<IEnumerable<FarmFilterNameModel>> GetNameFarm()
        {
            var farms = await repositoryManager.FarmRepository.GetAllFarm(false);
            var farmsDisplayModel = _mapper.Map<IEnumerable<FarmFilterNameModel>>(farms);
            return farmsDisplayModel;
        }

        public async Task<bool> RemoveFarm(int id)
        {
            try
            {
                _logger.LogInfomation($"Farm Service | Remove Farm: {id}");
                repositoryManager.FarmRepository.DeleteFarm(id);
                await repositoryManager.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Farm Service | Exception: {ex}");
                return false;
            }
        }
        public async Task<bool> UpdateFarm(FarmUpdateModel updateModel)
        {
            try
            {
                _logger.LogInfomation($"Farm Service | Udpate Farm: {updateModel}");
                repositoryManager.FarmRepository.UpdateFarm(updateModel);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Farm Service | Exception: {ex}");
                return false;
            }
        }
    }
}
