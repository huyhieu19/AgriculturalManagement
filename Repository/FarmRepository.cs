using Common.Queries;
using Dapper;
using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Contracts;
using Service.Contracts;

namespace Repository
{
    public sealed class FarmRepository : RepositoryBase<FarmEntity>, IFarmRepository
    {
        private readonly ILoggerManager logger;
        private readonly DapperContext dapperContext;
        public FarmRepository(FactDbContext factDbContext, ILoggerManager logger, DapperContext dapperContext) : base(factDbContext)
        {
            this.logger = logger;
            this.dapperContext = dapperContext;
        }

        public void CreateFarm(FarmEntity entity)
        {
            try
            {
                logger.LogInformation($"FarmRepository | Create | start ");
                Create(entity);
                logger.LogInformation($"FarmRepository | Create | end ");
            }
            catch (Exception ex)
            {
                logger.LogError($"FarmRepository | Create | Exeption: {ex}");
                throw;
            }
        }

        public void DeleteFarm(int id, string UserId)
        {
            try
            {
                logger.LogInformation($"FarmRepository | Delete: {id} | start ");
                var entity = FindByCondition(p => p.Id == id && p.UserId == UserId, false).First();
                Delete(entity);
                logger.LogInformation($"FarmRepository | Delete | end ");
            }
            catch (Exception ex)
            {
                logger.LogError($"FarmRepository | Delete | Exeption: {ex}");
                throw;
            }
        }

        public async Task<IEnumerable<FarmEntity>> GetAllFarm(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();

        public async Task<IEnumerable<FarmEntity>> GetFarms(string userId, bool trackchanges) => await FindByCondition(p => p.UserId == userId, trackchanges).OrderBy(p => p.Name).ToListAsync();

        public async void UpdateFarm(FarmUpdateModel model)
        {
            try
            {
                logger.LogInformation($"FarmRepository | Update: Id = {model.Id}| start ");
                var param = new DynamicParameters(model);

                using (var connection = dapperContext.CreateConnection())
                {
                    connection.Open();
                    using (var trans = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync(FarmQuery.UpdateFarmSQL, param, transaction: trans);
                        trans.Commit();
                    }
                    connection.Close();
                }

                logger.LogInformation($"FarmRepository | Update | end ");

            }
            catch (Exception ex)
            {
                logger.LogError($"FarmRepository | Delete | Exeption: {ex}");
                throw;
            }
        }
    }
}
