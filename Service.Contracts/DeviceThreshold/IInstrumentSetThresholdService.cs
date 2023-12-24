using Models;

namespace Service.Contracts.DeviceThreshold
{
    public interface IInstrumentSetThresholdService
    {
        Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffDelete();
        Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOff(string userId);
        Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffByIdDeviceDriver(Guid Id);
        Task<bool> DeviceInstrumentOnOffUpdate(InstrumentSetThresholdUpdateModel updateModel);
        Task<bool> DeviceInstrumentOnOffCreate(InstrumentSetThresholdCreateModel model);
        Task<bool> DeviceInstrumentOnOffDeleteById(int Id);
    }
}