using Models;
using Models.DeviceAuto;
using Models.DeviceControl;
using Models.DeviceTimer;

namespace Service.Contracts
{
    public interface IDeviceControlService
    {
        #region Đóng Mở thiết bị
        Task<bool> DeviceDriverOnOff(OnOffDeviceQueryModel model);

        Task<bool> SuccessJobTimer(int timerId, Guid deviceId);
        Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerAvailable();
        #endregion
    }
}