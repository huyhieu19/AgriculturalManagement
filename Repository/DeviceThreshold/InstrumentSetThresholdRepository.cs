﻿using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Models.InstrumentSetThreshold;
using Repository.Contracts.DeviceThreshold;

namespace Repository.DeviceThreshold
{
    public class InstrumentSetThresholdRepository : RepositoryBase<ThresholdDeviceEntity>, IInstrumentSetThresholdRepository
    {

        public InstrumentSetThresholdRepository(FactDbContext factDbContext) : base(factDbContext)
        {
        }

        public async Task<IEnumerable<ThresholdDeviceEntity>> DeviceInstrumentOnOffByIdDeviceDriver(Guid Id)
        {
            return await FindByCondition(p => p.DeviceDriverId == Id && !p.IsDelete, false).ToListAsync();
        }

        public async Task DeviceInstrumentOnOffCreate(ThresholdDeviceEntity model)
        {
            var records = await FindByCondition(p => p.DeviceDriverId == model.DeviceDriverId && p.InstrumentationId == model.InstrumentationId, false).ToListAsync();
            var device = await FactDbContext.DeviceEntities.FirstOrDefaultAsync(p => p.Id == model.DeviceDriverId);

            if (!records.Any())
            {
                if (device != null && device.IsAuto == false)
                {
                    device.IsAuto = true;
                    await FactDbContext.SaveChangesAsync();
                }
                Create(model);
            }
        }

        public async Task<bool> DeviceInstrumentOnOffDeleteById(ThresholdRemoveModel model)
        {
            var record = await FindByCondition(p => p.Id == model.Id, false).FirstOrDefaultAsync();
            if (record != null)
            {
                Delete(record);
            }
            return await FactDbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ThresholdDeviceEntity>> DeviceInstrumentOnOffDelete()
        {
            return await FindByCondition(p => p.IsDelete, false).ToListAsync();
        }

        public async Task<IEnumerable<ThresholdDeviceEntity>> DeviceInstrumentOnOffNotDelete()
        {
            return await FindByCondition(p => !p.IsDelete, false).ToListAsync();
        }

        public async Task DeviceInstrumentOnOffUpdate(ThresholdDeviceEntity model)
        {
            var record = await FindByCondition(p => p.DeviceDriverId == model.DeviceDriverId && p.InstrumentationId == model.InstrumentationId, false).FirstOrDefaultAsync();
            var device = await FactDbContext.DeviceEntities.FirstOrDefaultAsync(p => p.Id == model.DeviceDriverId);
            if (record != null)
            {
                if (device != null && device.IsAuto == false)
                {
                    device.IsAuto = true;
                    await FactDbContext.SaveChangesAsync();
                }
                Update(model);
            }
        }
    }
}