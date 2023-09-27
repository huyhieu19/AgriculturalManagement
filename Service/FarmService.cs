using AutoMapper;
using Entities;
using Models;
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
            try
            {
                _logger.LogInfomation("Create farm in Farm service layer");
                var companyEntity = _mapper.Map<FarmEntity>(createModel);
                repositoryManager.Farm.CreateFarm(companyEntity);
                await repositoryManager.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<IEnumerable<FarmDisplayModel>> GetAllFarmAsync(bool trackChanges)
        {
            try
            {
                _logger.LogInfomation("Get all farms in Service layer");
                var farms = await repositoryManager.Farm.GetAllFarm(trackChanges);
                var farmsDisplayModel = _mapper.Map<IEnumerable<FarmDisplayModel>>(farms);
                return farmsDisplayModel;
            }
            catch (Exception ex)
            {
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<IEnumerable<FarmDisplayModel>> GetByCondition(FarmQueryModel model, bool trackChanges)
        {
            try
            {
                _logger.LogInfomation($"Farm Service| Get By Condition | start ");

                var responseEntities = await repositoryManager.Farm.GetByCondition(model, trackChanges);
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
            try
            {
                var farms = await repositoryManager.Farm.GetAllFarm(false);
                var farmsDisplayModel = _mapper.Map<IEnumerable<FarmFilterNameModel>>(farms);
                return farmsDisplayModel;
            }
            catch (Exception ex)
            {
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<bool> RemoveFarm(int id, string UserId)
        {
            try
            {
                _logger.LogInfomation($"Farm Service | Remove Farm: {id}");
                repositoryManager.Farm.DeleteFarm(id, UserId);
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
                repositoryManager.Farm.UpdateFarm(updateModel);
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
