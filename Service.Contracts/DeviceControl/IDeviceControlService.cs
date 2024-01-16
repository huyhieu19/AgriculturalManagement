using Models.DeviceControl;
using Models.DeviceTimer;

namespace Service.Contracts
{
    public interface IDeviceControlService
    {
        #region Đóng Mở thiết bị
        Task<bool> DeviceDriverOnOff(OnOffDeviceQueryModel model);
        Task<bool> AsyncStatusDeviceControl();
        Task<bool> SuccessJobTurnOnOffDeviceTimer(int timerId, Guid deviceId, bool IsTurnOn);

        // Lấy tất cả giá trị chưa hoàn thành khi hẹn giờ
        Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerAvailable();
        #endregion
    }
}