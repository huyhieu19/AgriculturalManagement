using Database;
using Entities;
using Models;
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

        public Task<bool> CreateTypeDeviceDrivers(List<DeviceDriversTypeDisplayModel> model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTypeDeviceDrivers(List<int> Ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<DeviceDriverTypeEntity>> GetTypeDeviceDrivers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTypeDeviceDriver(DeviceDriversTypeDisplayModel model)
        {
            throw new NotImplementedException();
        }
    }
}
