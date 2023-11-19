using Models.DeviceControl;
using Models.DeviceTimer;

namespace Service.Contracts
{
    public interface IDeviceControlService
    {
        #region Đóng Mở thiết bị
        Task<bool> DeviceDriverOnOff(OnOffDeviceQueryModel model);

        Task<bool> SuccessJobTurnOnDeviceTimer(int timerId, Guid deviceId);
        Task<bool> SuccessJobTurnOffDeviceTimer(int timerId, Guid deviceId);
        Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerAvailable();
        #endregion
    }
}