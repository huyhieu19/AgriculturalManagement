using Database;
using Entities;
using Repository.Contracts;

namespace Repository
{
    public class DeviceDriverRepository : RepositoryBase<DeviceDriverEntity>, IDeviceDriverRepository
    {
        private readonly DapperContext dapperContext;
        public DeviceDriverRepository(FactDbContext factDbContext, DapperContext dapperContext) : base(factDbContext)
        {
            this.dapperContext = dapperContext;
        }

    }
}
