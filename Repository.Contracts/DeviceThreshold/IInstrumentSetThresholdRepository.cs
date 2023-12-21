using Entities;

namespace Repository.Contracts.DeviceThreshold
{
    public interface IInstrumentSetThresholdRepository
    {
        Task<IEnumerable<ThresholdDeviceEntity>> DeviceInstrumentOnOffNotDelete();
        Task<IEnumerable<ThresholdDeviceEntity>> DeviceInstrumentOnOffDelete();
        Task<IEnumerable<ThresholdDeviceEntity>> DeviceInstrumentOnOffByIdDeviceDriver(Guid Id);
        void DeviceInstrumentOnOffUpdate(ThresholdDeviceEntity updateModel);
        void DeviceInstrumentOnOffCreate(ThresholdDeviceEntity model);
        void DeviceInstrumentOnOffDeleteById(int Id);
    }
}