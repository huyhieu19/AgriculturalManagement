using Database;
using Entities;
using Models;
using Models.DeviceDriver;
using Repository.Contracts;

namespace Repository
{
    public sealed class DeviceDriverRepository : RepositoryBase<DeviceDriverEntity>, IDeviceDriverRepository
    {
        private readonly DapperContext dapperContext;
        private readonly FactDbContext factDbContext;
        public DeviceDriverRepository(FactDbContext factDbContext, DapperContext dapperContext) : base(factDbContext)
        {
            this.dapperContext = dapperContext;
            this.factDbContext = factDbContext;
        }

        public Task CreateDeviceDriver(InstrumentationCreateModel createModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDeviceDriver(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverByZoneAsync(int Id)
        {
            factDbContext.
        }

        public Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverNotInZoneAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveDeviceDriver(int Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInforDeviceDriver(InstrumentationUpdateModel updateModel)
        {
            throw new NotImplementedException();
        }
    }
}
