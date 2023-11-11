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

        public void DeleteEsp(Guid id)
        {
            Delete(new EspEntity()
            {
                Id = id
            });
        }

        public async Task<bool> AddEspToUser(Guid espId, string userId)
        {
            /*
             * Can use FindAsync or Where
             * 
             */
            var entity = await FindByCondition(p => p.Id == espId, true).FirstOrDefaultAsync();

            //var entity = await FactDbContext.Esp8266Entities.FindAsync(espId);

            if (entity == null)
            {
                throw new ArgumentException("Esp not exist");
            }

            if (entity.UserId == userId)
            {
                throw new ArgumentException("The Esp has been assigned to the user");
            }
            entity.UserId = userId;
            int change = await FactDbContext.SaveChangesAsync();
            return change == 1;
        }

        public async Task<List<EspEntity>> GetEsps(string id)
        {
            return await FindByCondition(p => p.UserId == id, false).Include(src => src.DeviceTypes).ToListAsync();
        }

        public async Task<List<EspEntity>> GetEspsAll()
        {
            var entities = await FindAll(false).Include(src => src.DeviceTypes).ToListAsync();
            return entities;
        }
    }
}