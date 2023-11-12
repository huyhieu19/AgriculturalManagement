using Common.Enum;
using Database;
using Entities.Module;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository
{
    public class MockDataRepository : IMockDataRepository
    {
        private readonly FactDbContext factDbContext;

        public MockDataRepository(FactDbContext factDbContext)
        {
            this.factDbContext = factDbContext;
        }

        public async Task<bool> DeleteMockDataDeviceOnModule(Guid moduleId)
        {
            var entities = await factDbContext.DeviceEntities.Where(prop => prop.ModuleId == moduleId).ToListAsync();
            factDbContext.DeviceEntities.RemoveRange(entities);
            return await factDbContext.SaveChangesAsync() > 0;
        }

        public async Task<ModuleType> GetTypeModule(Guid moduleId)
        {
            var entity = await factDbContext.ModuleEntities.FindAsync(moduleId);
            if (entity != null)
            {
                return entity.ModuleType;
            }
            throw new ArgumentNullException("Module not found");
        }

        public async Task<bool> MockDevicesOnModule(List<DeviceEntity> devices)
        {
            await factDbContext.DeviceEntities.AddRangeAsync(devices);
            return await factDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> MockModule(ModuleEntity entity)
        {
            await factDbContext.ModuleEntities.AddAsync(entity);
            return await factDbContext.SaveChangesAsync() > 0;
        }
    }
}
