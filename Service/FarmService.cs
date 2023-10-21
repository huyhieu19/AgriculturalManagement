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
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly DapperContext dapperContext;
        public FarmService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, DapperContext dapperContext)
        {
            this.repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
            this.dapperContext = dapperContext;
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

        public async Task<IEnumerable<FarmDisplayModel>> GetFarms(string userId, bool trackChanges)
        {
            try
            {
                _logger.LogInfomation($"Farm Service| Get farm by user | start ");
                string query = FarmQuery.GetFarmSQL;
                IEnumerable<FarmDisplayModel> response;
                using (var connection = dapperContext.CreateConnection())
                {
                    response = await connection.QueryAsync<FarmDisplayModel>(query, new { UserID = userId });
                }
                _logger.LogInfomation($"Farm Service | Get farm by user | end ");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Farm Service | Delete | Exeption: {ex}");
                throw;
            }
        }

        public async Task<IEnumerable<FarmFilterNameModel>> GetNameFarm(string userId)
        {
            try
            {
                var farms = await repositoryManager.Farm.GetFarms(userId, false);
                var farmsDisplayModel = _mapper.Map<IEnumerable<FarmFilterNameModel>>(farms);
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
                _logger.LogInfomation($"Farm Service | Remove Farm: {id}");
                repositoryManager.Farm.DeleteFarm(id, userId);
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
