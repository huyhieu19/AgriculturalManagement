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

        //public async Task<List<TimerDeviceEntity>> GetAllTimerHistory()
        //{
        //    return await FindByCondition(prop => prop.IsSuccessON || prop.IsSuccessOFF || prop.IsRemove || (prop.OpenTimer < DateTime.UtcNow && prop.ShutDownTimer < DateTime.UtcNow), false).ToListAsync();
        //}

        public async Task<List<TimerDeviceEntity>> GetAllTimerHistoryByDeviceId(Guid deviceId)
        {
            return await FindByCondition(prop => prop.DeviceId == deviceId && (prop.IsSuccessON || prop.IsSuccessOFF || prop.IsRemove || (prop.OpenTimer < DateTime.UtcNow && prop.ShutDownTimer < DateTime.UtcNow)), false).ToListAsync();
        }

        public Task<List<TimerDeviceDriverDisplayModel>> GetTimerAvailableOfUserForUI(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateTimer(TimerDeviceDriverUpdateModel model)
        {
            var entity = await FactDbContext.TimerDeviceDriverEntities.FindAsync(model.Id);
            if (entity != null)
            {
                entity.Note = model.Note;
                entity.ShutDownTimer = model.ShutDownTimer;
                entity.DateUpdated = DateTime.UtcNow;
                entity.OpenTimer = model.OpenTimer;
            }
            return await FactDbContext.SaveChangesAsync() > 0;
        }
    }
}