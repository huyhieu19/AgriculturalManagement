using Models.DeviceTimer;

namespace Service.Contracts.DeviceTimer
{
    public interface IDeviceTimerService
    {

        #region Timer
        // Set Timer for device driver
        Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimer();
        Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerByDeviceId(int Id);
        Task CreateTimer(TimerDeviceDriverCreateModel model);
        Task UpdateTimer(TimerDeviceDriverDisplayModel model);
        Task DeleteTimer(int Id);
        Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerHistoryByDeviceId(int Id);
        Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerHistory();
        #endregion

    }
}