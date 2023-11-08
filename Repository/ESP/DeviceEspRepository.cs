using Database;
using Entities.ESP;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository.ESP
{
    public class DeviceEspRepository : RepositoryBase<DeviceTypeOnEspEntity>, IDeviceEspRepository
    {
        public DeviceEspRepository(FactDbContext factDbContext) : base(factDbContext)
        {
        }


        public async Task<List<DeviceTypeOnEspEntity>> DeviceESPDisplay(Guid id)
        {
            return await FindByCondition(p => p.Id == id, false).ToListAsync();
        }


        public void DeviceESPCreate(DeviceTypeOnEspEntity entity)
        {
            Create(entity);
        }

        public void DeviceESPRemove(Guid id)
        {
            Delete(new DeviceTypeOnEspEntity()
            {
                Id = id
            });
        }
    }
}
