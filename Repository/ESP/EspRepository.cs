using Database;
using Entities.ESP;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository
{
    public class EspRepository : RepositoryBase<EspEntity>, IEspRepository
    {
        public EspRepository(FactDbContext factDbContext) : base(factDbContext)
        {
        }

        public void CreateEsp(EspEntity entity)
        {
            Create(entity);
        }

        public async Task<List<EspEntity>> GetAll()
        {
            return await FactDbContext.Esp8266Entities.ToListAsync();
        }
    }
}