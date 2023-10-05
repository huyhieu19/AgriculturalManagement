using Models;

namespace Service.Contracts
{
    public interface IInstrumentSetThresholdService
    {
        Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffNotDelete();
        Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffDelete();
        Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOff();
        Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffByIdDeviceDriver(int Id);
        Task DeviceInstrumentOnOffUpdate(InstrumentSetThresholdUpdateModel updateModel);
        Task DeviceInstrumentOnOffCreate(InstrumentSetThresholdCreateModel model);
        Task DeviceInstrumentOnOffDeleteById(int Id);
    }
}
