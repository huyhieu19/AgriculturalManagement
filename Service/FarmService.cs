using AutoMapper;
using Common.Queries;
using Dapper;
using Database;
using Entities;
using Models;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class FarmService : IFarmService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;
        private readonly DapperContext dapperContext;
        public FarmService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, DapperContext dapperContext)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
            this.dapperContext = dapperContext;
        }

        public async Task<bool> AddFarm(FarmCreateModel createModel)
        {
            try
            {
                logger.LogInformation("Create farm in Farm service layer");
                var companyEntity = mapper.Map<FarmEntity>(createModel);
                repositoryManager.Farm.CreateFarm(companyEntity);
                int isChange = await repositoryManager.SaveAsync();
                return isChange > 0;
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
                logger.LogInformation("Get all farms in Service layer");
                var farms = await repositoryManager.Farm.GetAllFarm(trackChanges);
                var farmsDisplayModel = mapper.Map<IEnumerable<FarmDisplayModel>>(farms);
                return farmsDisplayModel;
            }
            catch (Exception ex)
            {
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<IEnumerable<FarmDisplayModel>> GetFarms(string userId, bool trackChanges)
        {
            try
            {
                logger.LogInformation($"Farm Service| Get farm by user | start ");
                string query = FarmQuery.GetFarmSQL;
                IEnumerable<FarmDisplayModel> response;
                using (var connection = dapperContext.CreateConnection())
                {
                    response = await connection.QueryAsync<FarmDisplayModel>(query, new { UserID = userId });
                }
                logger.LogInformation($"Farm Service | Get farm by user | end ");
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError($"Farm Service | Delete | Exeption: {ex}");
                throw;
            }
        }

        public async Task<IEnumerable<FarmFilterNameModel>> GetNameFarm(string userId)
        {
            try
            {
                var farms = await repositoryManager.Farm.GetFarms(userId, false);
                var farmsDisplayModel = mapper.Map<IEnumerable<FarmFilterNameModel>>(farms);
                return farmsDisplayModel;
            }
            catch (Exception ex)
            {
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<bool> RemoveFarm(int id, string userId)
        {
            try
            {
                logger.LogInformation($"Farm Service | Remove Farm: {id}");
                repositoryManager.Farm.DeleteFarm(id, userId);
                int isChange = await repositoryManager.SaveAsync();
                return isChange > 0;
            }
            catch (Exception ex)
            {
                logger.LogError($"Farm Service | Exception: {ex}");
                return false;
            }
        }
        public async Task<bool> UpdateFarm(FarmUpdateModel updateModel)
        {
            try
            {
                logger.LogInformation($"Farm Service | Udpate Farm: {updateModel}");
                var entity = mapper.Map<FarmEntity>(updateModel);
                repositoryManager.Farm.UpdateFarm(entity);
                int isChange = await repositoryManager.SaveAsync();
                return isChange > 0;
            }
            catch (Exception ex)
            {
                logger.LogError($"Farm Service | Exception: {ex}");
                return false;
            }
        }
    }
}
