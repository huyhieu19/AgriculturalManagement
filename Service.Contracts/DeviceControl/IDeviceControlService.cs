using Models;
using Models.DeviceAuto;

namespace Service.Contracts
{
    public interface IDeviceControlService
    {
        #region Device
        // Đóng thiết bị
        Task<bool> DeviceDriverTurnOff(Guid DeviceId);
        // Mở thiết bị
        Task DeviceDriverTurnOn(Guid DeviceId);
        #endregion

        #region Timer
        Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffNotDelete();
        #endregion
    }
}