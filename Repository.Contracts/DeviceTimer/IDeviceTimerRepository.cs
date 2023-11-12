using Entities;
using Models.DeviceTimer;

namespace Repository.Contracts.DeviceTimer
{
    public interface IDeviceTimerRepository
    {
        void CreateTimer(TimerDeviceDriverEntity entity);
        Task<bool> UpdateTimer(TimerDeviceDriverUpdateModel entity);
        Task<List<TimerDeviceDriverEntity>> GetAllTimerHistoryByDeviceId(Guid deviceId);
        Task<List<TimerDeviceDriverEntity>> GetAllTimerHistory();
    }
}