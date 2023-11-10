using Database;
using Entities.ESP;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository.ESP
{
    public class DeviceEspRepository : RepositoryBase<DeviceTypeEspEntity>, IDeviceEspRepository
    {
        public DeviceEspRepository(FactDbContext factDbContext) : base(factDbContext)
        {
        }


        public async Task<List<DeviceTypeEspEntity>> DeviceESPDisplay(Guid id)
        {
            return await FindByCondition(p => p.EspId == id, false).Include(src => src.DeviceDriver).Include(src => src.Instrumentation).ToListAsync();
        }


        public void DeviceESPCreate(DeviceTypeEspEntity entity)
        {
            Create(entity);
        }

        public void DeviceESPRemove(Guid id)
        {
            Delete(new DeviceTypeEspEntity()
            {
                Id = id
            });
        }
    }
}
