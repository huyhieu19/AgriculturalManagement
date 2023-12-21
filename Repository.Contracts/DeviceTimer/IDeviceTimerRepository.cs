using Entities;
using Models.DeviceTimer;

namespace Repository.Contracts.DeviceTimer
{
    public interface IDeviceTimerRepository
    {
        Task<List<TimerDeviceDriverDisplayModel>> GetTimerAvailableOfUserForUI(string userId);
        void CreateTimer(TimerDeviceEntity entity);
        Task<bool> UpdateTimer(TimerDeviceDriverUpdateModel entity);
        Task<List<TimerDeviceEntity>> GetAllTimerHistoryByDeviceId(Guid deviceId);
        //Task<List<TimerDeviceEntity>> GetAllTimerHistory();
    }
}