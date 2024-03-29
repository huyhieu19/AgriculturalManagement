﻿using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts.Farm;

namespace Repository.FarmZone
{
    public class ZoneRepository : RepositoryBase<ZoneEntity>, IZoneRepository
    {

        public ZoneRepository(FactDbContext factDbContext) : base(factDbContext)
        {
        }

        public void CreateZone(ZoneEntity entity) => Create(entity);

        public void DeleteZone(int id, int farmId)
        {
            Delete(new ZoneEntity() { Id = id, FarmId = farmId });
        }

        public async Task<IEnumerable<ZoneEntity>> GetZones(int farmId, bool trackChanges)
        {
            var zones = await FindByCondition(p => p.FarmId == farmId, trackChanges).Include(p => p.Devices).OrderBy(prop => prop.DateCreated).OrderBy(p => p.ZoneName).ToListAsync();
            return zones;
        }

        public void UpdateZone(ZoneEntity entity) => Update(entity);
    }
}