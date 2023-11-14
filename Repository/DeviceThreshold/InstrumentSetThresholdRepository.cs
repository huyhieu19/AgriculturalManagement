using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts.DeviceThreshold;

namespace Repository.DeviceThreshold
{
    public class InstrumentSetThresholdRepository : RepositoryBase<ThresholdDeviceEntity>, IInstrumentSetThresholdRepository
    {

        public InstrumentSetThresholdRepository(FactDbContext factDbContext) : base(factDbContext)
        {
        }

        public async Task<IEnumerable<ThresholdDeviceEntity>> DeviceInstrumentOnOff()
        {
            return await FindAll(false).ToListAsync();
        }

        public async Task<IEnumerable<ThresholdDeviceEntity>> DeviceInstrumentOnOffByIdDeviceDriver(Guid Id)
        {
            return await FindByCondition(p => p.DeviceDriverId == Id && !p.IsDelete, false).ToListAsync();
        }

        public void DeviceInstrumentOnOffCreate(ThresholdDeviceEntity model)
        {
            Create(model);
        }

        public void DeviceInstrumentOnOffDeleteById(int Id)
        {
            Update(new ThresholdDeviceEntity() { Id = Id, IsDelete = true });
        }

        public async Task<IEnumerable<ThresholdDeviceEntity>> DeviceInstrumentOnOffDelete()
        {
            return await FindByCondition(p => p.IsDelete, false).ToListAsync();
        }

        public async Task<IEnumerable<ThresholdDeviceEntity>> DeviceInstrumentOnOffNotDelete()
        {
            return await FindByCondition(p => !p.IsDelete, false).ToListAsync();
        }

        public void DeviceInstrumentOnOffUpdate(ThresholdDeviceEntity updateModel)
        {
            Update(updateModel);
        }
    }
}