using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository
{
    public class InstrumentSetThresholdRepository : RepositoryBase<DeviceInstrumentOnOffEntity>, IInstrumentSetThresholdRepository
    {
        //private readonly DapperContext dapperContext;
        //private readonly FactDbContext factDbContext;

        public InstrumentSetThresholdRepository(FactDbContext factDbContext) : base(factDbContext)
        {
            //this.dapperContext = dapperContext;
            //this.factDbContext = factDbContext;
        }

        public async Task<IEnumerable<DeviceInstrumentOnOffEntity>> DeviceInstrumentOnOff()
        {
            return await FindAll(false).ToListAsync();
        }

        public async Task<IEnumerable<DeviceInstrumentOnOffEntity>> DeviceInstrumentOnOffByIdDeviceDriver(int Id)
        {
            return await FindByCondition(p => p.DeviceDriverId == Id && !p.IsDelete, false).ToListAsync();
        }

        public void DeviceInstrumentOnOffCreate(DeviceInstrumentOnOffEntity model)
        {
            Create(model);
        }

        public void DeviceInstrumentOnOffDeleteById(int Id)
        {
            Update(new DeviceInstrumentOnOffEntity() { Id = Id, IsDelete = true });
        }

        public async Task<IEnumerable<DeviceInstrumentOnOffEntity>> DeviceInstrumentOnOffDelete()
        {
            return await FindByCondition(p => p.IsDelete, false).ToListAsync();
        }

        public async Task<IEnumerable<DeviceInstrumentOnOffEntity>> DeviceInstrumentOnOffNotDelete()
        {
            return await FindByCondition(p => !p.IsDelete, false).ToListAsync();
        }

        public void DeviceInstrumentOnOffUpdate(DeviceInstrumentOnOffEntity updateModel)
        {
            Update(updateModel);
        }
    }
}
