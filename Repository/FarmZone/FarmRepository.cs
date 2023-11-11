using Database;
using Entities.Farm;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Contracts.Farm;
using Service.Contracts.Logger;

namespace Repository.FarmZone
{
    public sealed class FarmRepository : RepositoryBase<FarmEntity>, IFarmRepository
    {
        private readonly ILoggerManager logger;
        public FarmRepository(FactDbContext factDbContext, ILoggerManager logger) : base(factDbContext)
        {
            this.logger = logger;
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
                logger.LogError($"FarmRepository | Create | Exception: {ex}");
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
                logger.LogError($"FarmRepository | Delete | Exception: {ex}");
                throw;
            }
        }

        public async Task<List<FarmDisplayModel>> GetFarms(string userId, bool trackchanges)
        {
            var query = await FactDbContext.FarmEntities
                .Where(f => f.UserId == userId)
                .GroupJoin(FactDbContext.ZoneEntityEntities,
                    f => f.Id,
                    z => z.FarmId,
                    (farm, zones) => new FarmDisplayModel
                    {
                        Id = farm.Id,
                        Name = farm.Name,
                        Description = farm.Description,
                        Address = farm.Address,
                        Note = farm.Note,
                        DateCreated = farm.DateCreated,
                        Area = farm.Area,
                        CountZone = zones.Count()
                    }).OrderBy(prop => prop.DateCreated).ThenBy(prop => prop.Name)
                .ToListAsync();
            return query;
        }


        public void UpdateFarm(FarmEntity entity)
        {
            Update(entity);
        }
    }
}