using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository
{
    public class ZoneRepository : RepositoryBase<ZoneEntity>, IZoneRepository
    {

        public ZoneRepository(FactDbContext factDbContext) : base(factDbContext)
        {
        }

        public void CreateZone(ZoneEntity entity) => Create(entity);

        public void DeleteZone(int id)
        {
            //var entity = FindByCondition(p => p.Id == id, false).First();

            Delete(new ZoneEntity() { Id = id });
            //if (entity != null)
            //{
            //}
        }

        public async Task<IEnumerable<ZoneEntity>> GetZones(int farmId, bool trackchanges) => await FindByCondition(p => p.FarmId == farmId, trackchanges).OrderBy(p => p.ZoneName).ToListAsync();


        public void UpdateZone(ZoneEntity entity) => Update(entity);
    }
}