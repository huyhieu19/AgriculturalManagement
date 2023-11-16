using Models;

namespace Service.Contracts
{
    public interface IDeviceDriverService
    {
        #region Device
        //Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverByZoneAsync(int Id);
        //Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverNotInZoneAsync();
        //Task UpdateInforDeviceDriver(DeviceDriverUpdateModel updateModel);
        //Task CreateDeviceDriver(DeviceDriverCreateModel createModel);
        //Task DeleteDeviceDriver(int Id); //  Xóa hẳn => bị hỏng máy
        //Task RemoveDeviceDriver(int Id); //chuyển Zone Id  = null


        #endregion

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