using Database;
using Entities.Module;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository
{
    public class ModuleRepository : RepositoryBase<ModuleEntity>, IModuleRepository
    {
        public ModuleRepository(FactDbContext factDbContext) : base(factDbContext)
        {
        }

        public async Task RemoveModuleFromUser(Guid moduleId, string userId)
        {
            var entity = await FindByCondition(prop => prop.Id == moduleId && prop.UserId == userId, true).FirstOrDefaultAsync();
            if (entity != null)
            {
                entity.UserId = null;
            }
            else
            {
                throw new ArgumentNullException("Module not found");
            }
        }

        public async Task<bool> AddModuleToUser(Guid espId, string userId)
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

        public async Task<List<ModuleEntity>> GetModules(string userId)
        {
            return await FindByCondition(p => p.UserId == userId, false).Include(src => src.Devices!.OrderBy(prop => prop.Gate)).ToListAsync();
        }

        public async Task<List<ModuleEntity>> GetModulesAll()
        {
            var entities = await FindAll(false).Include(src => src.Devices!.OrderBy(prop => prop.Gate)).ToListAsync();
            return entities;
        }
    }
}