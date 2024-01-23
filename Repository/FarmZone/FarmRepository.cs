using Common.Enum;
using Database;
using Entities.Farm;
using EnumsNET;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DeviceTimer;
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

        #region Get Farm, Zone, Device for process add timer to device 
        public async Task<DeviceDriverByFarmDisplayModel> DeviceDriverByFarmZone(string userId, DeviceType deviceType)
        {
            var DeviceDriverByFarm = new DeviceDriverByFarmDisplayModel();
            var farms = await GetFarms(userId, false);
            var farmIds = farms.Select(p => p.Id).ToList();
            var zones = await FactDbContext.ZoneEntityEntities.Where(p => farmIds.Contains(p.FarmId ?? -1)).ToListAsync();
            var zoneIds = farms.Select(p => p.Id).ToList();
            var devices = await FactDbContext.DeviceEntities.Where(p => zoneIds.Contains(p.ZoneId ?? -1) && p.DeviceType == deviceType.AsString(EnumFormat.Description)).ToListAsync();


            DeviceDriverByFarm.Farms.AddRange(
                farms.Select(p => new KeyValueForFarm()
                {
                    Id = p.Id,
                    Name = p.Name,
                }).ToList()
            );

            DeviceDriverByFarm.Zone.AddRange(
                zones.Select(p => new KeyValueForZone()
                {
                    Id = p.Id,
                    Name = p.ZoneName,
                    FarmId = p.FarmId,
                }).ToList()
            );
            DeviceDriverByFarm.Device.AddRange(
                devices.Select(p => new KeyValueForDevice()
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    ZoneId = p.ZoneId,
                    NameRef = p.NameRef,
                }).ToList()
            );

            return DeviceDriverByFarm;
        }
        #endregion
    }
}