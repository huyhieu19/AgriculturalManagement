using Database;
using Entities;
using Entities.ESP;
using Repository.Contracts.DeviceTimer;

namespace Repository.DeviceTimer
{
    public sealed class DeviceDriverRepository : RepositoryBase<DeviceEntity>, IDeviceDriverRepository
    {
        private readonly DapperContext dapperContext;
        private readonly FactDbContext factDbContext;
        public DeviceDriverRepository(FactDbContext factDbContext, DapperContext dapperContext) : base(factDbContext)
        {
            this.dapperContext = dapperContext;
            this.factDbContext = factDbContext;
        }


        public async void CreateTimer(TimerDeviceDriverEntity entity)
        {
            await factDbContext.TimerDeviceDriverEntities.AddAsync(entity);
        }

        public void UpdateTimer(TimerDeviceDriverEntity entity)
        {
            factDbContext.TimerDeviceDriverEntities.Update(entity);
        }
    }
}