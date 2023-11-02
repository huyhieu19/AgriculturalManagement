using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository
{
    public sealed class DeviceDriverTypeRepository : RepositoryBase<DeviceDriverTypeEntity>, IDeviceDriverTypeRepository
    {
        private readonly DapperContext dapperContext;
        public DeviceDriverTypeRepository(FactDbContext factDbContext, DapperContext dapperContext) : base(factDbContext)
        {
            this.dapperContext = dapperContext;
        }

        public void CreateTypeDeviceDrivers(DeviceDriverTypeEntity entity)
        {
            Create(entity);
        }

        public void DeleteTypeDeviceDrivers(DeviceDriverTypeEntity entity)
        {
            Delete(entity);
        }

        public async Task<List<DeviceDriverTypeEntity>> GetTypeDeviceDrivers()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public void UpdateTypeDeviceDriver(DeviceDriverTypeEntity entity)
        {
            Update(entity);
        }
    }
}