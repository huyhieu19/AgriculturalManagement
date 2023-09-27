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
                logger.LogInfomation($"FarmRepository | Create | start ");
                Create(entity);
                logger.LogInfomation($"FarmRepository | Create | end ");
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
                logger.LogInfomation($"FarmRepository | Delete: {id} | start ");
                var entity = FindByCondition(p => p.Id == id && p.UserId == UserId, false).First();
                Delete(entity);
                logger.LogInfomation($"FarmRepository | Delete | end ");
            }
            catch (Exception ex)
            {
                logger.LogError($"FarmRepository | Delete | Exeption: {ex}");
                throw;
            }
        }

        public async Task<IEnumerable<FarmEntity>> GetAllFarm(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();

        public async Task<IEnumerable<FarmEntity>> GetByCondition(FarmQueryModel  model, bool trackchanges)
        {
            var Farms = await FindByCondition(p => p.UserId == model.UserId, trackchanges).ToListAsync();

            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                string key = model.SearchTerm;
                Farms = Farms.Where(p => p.Name.ToLower().Contains(key.ToLower())).ToList();
            }
            if (model.typeOrderBy is Common.Enum.TypeOrderBy.AToZByName)
            {
                return Farms.OrderBy(p => p.Name).ToList();
            }
            else if (model.typeOrderBy is Common.Enum.TypeOrderBy.ZToAByName)
            {
                return Farms.OrderByDescending(p => p.Name).ToList();
            }
            else if (model.typeOrderBy is Common.Enum.TypeOrderBy.IncreasingDay)
            {
                return Farms.OrderByDescending(p => p.CreatedDate).ToList();
            }
            return Farms.OrderBy(p => p.Name).ToList();
        }

        public async void UpdateFarm(FarmUpdateModel model)
        {
            try
            {
                logger.LogInfomation($"FarmRepository | Update: Id = {model.Id}| start ");
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

                logger.LogInfomation($"FarmRepository | Update | end ");

            }
            catch (Exception ex)
            {
                logger.LogError($"FarmRepository | Delete | Exeption: {ex}");
                throw;
            }
        }
    }
}
