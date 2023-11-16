using Models;

namespace Service.Contracts.DeviceThreshold
{
    public interface IInstrumentSetThresholdService
    {
        Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffDelete();
        Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOff();
        Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffByIdDeviceDriver(Guid Id);
        Task DeviceInstrumentOnOffUpdate(InstrumentSetThresholdUpdateModel updateModel);
        Task DeviceInstrumentOnOffCreate(InstrumentSetThresholdCreateModel model);
        Task DeviceInstrumentOnOffDeleteById(int Id);
    }
}