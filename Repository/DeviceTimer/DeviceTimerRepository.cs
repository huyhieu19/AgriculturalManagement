using Common.TimeHelper;
using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Models.DeviceTimer;
using Repository.Contracts.DeviceTimer;

namespace Repository.DeviceTimer
{
    public sealed class DeviceTimerRepository : RepositoryBase<TimerDeviceEntity>, IDeviceTimerRepository
    {
        public DeviceTimerRepository(FactDbContext factDbContext) : base(factDbContext)
        {
        }


        public void CreateTimer(TimerDeviceEntity entity)
        {
            Create(entity);
        }

        public async Task<List<TimerDeviceEntity>> GetAllTimerHistory()
        {
            return await FindByCondition(prop => prop.IsSuccess || prop.IsRemove, false).ToListAsync();
        }

        public async Task<List<TimerDeviceEntity>> GetAllTimerHistoryByDeviceId(Guid deviceId)
        {
            return await FindByCondition(prop => prop.DeviceId == deviceId && (prop.IsSuccess || prop.IsRemove), false).ToListAsync();
        }

        public async Task<bool> UpdateTimer(TimerDeviceDriverUpdateModel model)
        {
            var entity = await FactDbContext.TimerDeviceDriverEntities.FindAsync(model.Id);
            if (entity != null)
            {
                entity.Note = model.Note;
                entity.ShutDownTimer = model.ShutDownTimer;
                entity.DateUpdated = SetTimeZone.GetDateTimeVN();
                entity.OpenTimer = model.OpenTimer;
            }
            return await FactDbContext.SaveChangesAsync() > 0;
        }
    }
}