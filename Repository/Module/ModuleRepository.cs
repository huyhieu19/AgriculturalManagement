using Database;
using Entities.Module;
using Microsoft.EntityFrameworkCore;
using Models;
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

        public async Task<bool> AddModuleToUser(Guid moduleId, string userId, string nameRef)
        {
            /*
             * Can use FindAsync or Where
             * 
             */
            var entity = await FindByCondition(p => p.Id == moduleId, true).FirstOrDefaultAsync();

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
            entity.NameRef = nameRef;
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

        public async Task<bool> EditModule(ModuleUpdateModel model)
        {
            var entity = await FindByCondition(prop => prop.Id == model.Id, true).FirstOrDefaultAsync();
            if (entity == null)
            {
                return false;
            }
            else
            {
                entity.NameRef = model.NameRef;
                entity.Note = model.Note;
            }
            return await FactDbContext.SaveChangesAsync() > 0;
        }
    }
}