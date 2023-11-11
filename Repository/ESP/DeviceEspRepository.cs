using Database;
using Entities.ESP;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository.ESP
{
    public class DeviceEspRepository : RepositoryBase<DeviceEntity>, IDeviceEspRepository
    {
        public DeviceEspRepository(FactDbContext factDbContext) : base(factDbContext)
        {
        }


        public async Task<List<DeviceEntity>> DeviceESPDisplay(Guid id)
        {
            return await FindByCondition(p => p.EspId == id, false).Include(src => src.DeviceInstrumentOnOffs).ToListAsync();
        }


        public void DeviceESPCreate(DeviceEntity entity)
        {
            Create(entity);
        }

        public void DeviceESPRemove(Guid id)
        {
            Delete(new DeviceEntity()
            {
                Id = id
            });
        }
    }
}
