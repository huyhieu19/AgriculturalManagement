using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Logging;
using Models;
using Repository.Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FarmRepository : RepositoryBase<FarmEntity>, IFarmRepository
    {
        private readonly ILoggerManager logger;
        public FarmRepository(FactDbContext factDbContext, ILoggerManager logger) : base(factDbContext)
        {
            this.logger = logger;   
        }

        public Task CreateFarm(FarmEntity entity)
        {
            try
            {
                logger.LogInfomation($"FarmRepository | Create {entity} | start ");
                Create(entity);
                FactDbContext.SaveChanges();
                logger.LogInfomation($"FarmRepository | Create | end ");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                logger.LogError($"FarmRepository | Create | Exeption: {ex}");
                throw;
            }
        }

        public Task DeleteFarm(int id)
        {
            try
            {
                logger.LogInfomation($"FarmRepository | Delete: {id} | start ");
                var entity = FindByCondition(p => p.Id == id, false).First();
                Delete(entity);
                FactDbContext.SaveChanges();
                logger.LogInfomation($"FarmRepository | Delete | end ");
                return Task.CompletedTask;
            }catch (Exception ex)
            {
                logger.LogError($"FarmRepository | Delete | Exeption: {ex}");
                throw;
            }
        }

        public async Task<IEnumerable<FarmEntity>> GetAllFarm(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();

        public async Task<IEnumerable<FarmEntity>> GetByCondition(QueryBaseModel model, bool trackchanges)
        {
            var Farms = await FindAll(trackchanges).ToListAsync();
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

        public async Task UpdateFarm(FarmEntity entity)
        {
            try
            {
                logger.LogInfomation($"FarmRepository | Update: {entity}| start ");
                var entityUpdate = await FindByCondition(p => p.Id == entity.Id, false).FirstOrDefaultAsync();
                if (entityUpdate != null) {
                    Update(entity); 
                    FactDbContext.SaveChanges();
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
