using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository
{
    public class InstrumentSetThresholdRepository : RepositoryBase<DeviceInstrumentThresholdEntity>, IInstrumentSetThresholdRepository
    {
        //private readonly DapperContext dapperContext;
        //private readonly FactDbContext factDbContext;

        public InstrumentSetThresholdRepository(FactDbContext factDbContext) : base(factDbContext)
        {
            //this.dapperContext = dapperContext;
            //this.factDbContext = factDbContext;
        }

        public async Task<IEnumerable<DeviceInstrumentThresholdEntity>> DeviceInstrumentOnOff()
        {
            return await FindAll(false).ToListAsync();
        }

        public async Task<IEnumerable<DeviceInstrumentThresholdEntity>> DeviceInstrumentOnOffByIdDeviceDriver(int Id)
        {
            return await FindByCondition(p => p.DeviceDriverId == Id && !p.IsDelete, false).ToListAsync();
        }

        public void DeviceInstrumentOnOffCreate(DeviceInstrumentThresholdEntity model)
        {
            Create(model);
        }

        public void DeviceInstrumentOnOffDeleteById(int Id)
        {
            Update(new DeviceInstrumentThresholdEntity() { Id = Id, IsDelete = true });
        }

        public async Task<IEnumerable<DeviceInstrumentThresholdEntity>> DeviceInstrumentOnOffDelete()
        {
            return await FindByCondition(p => p.IsDelete, false).ToListAsync();
        }

        public async Task<IEnumerable<DeviceInstrumentThresholdEntity>> DeviceInstrumentOnOffNotDelete()
        {
            return await FindByCondition(p => !p.IsDelete, false).ToListAsync();
        }

        public void DeviceInstrumentOnOffUpdate(DeviceInstrumentThresholdEntity updateModel)
        {
            Update(updateModel);
        }
    }
}
