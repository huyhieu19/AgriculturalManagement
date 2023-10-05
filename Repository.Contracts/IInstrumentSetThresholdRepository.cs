using Entities;

namespace Repository.Contracts
{
    public interface IInstrumentSetThresholdRepository
    {
        Task<IEnumerable<DeviceInstrumentOnOffEntity>> DeviceInstrumentOnOff();
        Task<IEnumerable<DeviceInstrumentOnOffEntity>> DeviceInstrumentOnOffNotDelete();
        Task<IEnumerable<DeviceInstrumentOnOffEntity>> DeviceInstrumentOnOffDelete();
        Task<IEnumerable<DeviceInstrumentOnOffEntity>> DeviceInstrumentOnOffByIdDeviceDriver(int Id);
        void DeviceInstrumentOnOffUpdate(DeviceInstrumentOnOffEntity updateModel);
        void DeviceInstrumentOnOffCreate(DeviceInstrumentOnOffEntity model);
        void DeviceInstrumentOnOffDeleteById(int Id);
    }
}
