using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository
{
    public class EspRepository : RepositoryBase<Esp8266Entity>, IEspRepository
    {
        public EspRepository(FactDbContext factDbContext) : base(factDbContext)
        {
        }

        public void CreateEsp(Esp8266Entity entity)
        {
            Create(entity);
        }

        public async Task<List<Esp8266Entity>> GetAll()
        {
            return await FactDbContext.Esp8266Entities.Include(p => p.DeviceDrivers).Include(p => p.Instrumentations).ToListAsync();
        }
    }
}
