using Models.DeviceControl;
using Models.DeviceTimer;

namespace Service.Contracts
{
    public interface IDeviceControlService
    {
        #region Đóng Mở thiết bị
        Task<bool> DeviceDriverOnOff(OnOffDeviceQueryModel model);
        Task<bool> AsyncStatusDeviceControl();


        Task<bool> SuccessJobTurnOnDeviceTimer(int timerId, Guid deviceId);
        Task<bool> SuccessJobTurnOffDeviceTimer(int timerId, Guid deviceId);

        // Lấy tất cả giá trị chưa hoàn thành khi hẹn giờ
        Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerAvailable();
        #endregion
    }
}