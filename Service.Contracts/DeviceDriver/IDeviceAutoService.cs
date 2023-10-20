using Models;

namespace Service.Contracts
{
    public interface IDeviceAutoService
    {
        #region Device
        // Đóng thiết bị
        Task DeviceDriverTurnOff(int DeviceDriverId);
        // Mở thiết bị
        Task DeviceDriverTurnOn(int DeviceDriverId);
        Task<IEnumerable<DeviceDriverTurnOnTurnOffModel>> GetDeviceDriverTurnOnTurnOffModels();
        #endregion

        #region Timer
        Task DeleteTimer(int Id);

        Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffNotDelete();
        #endregion
    }
}
