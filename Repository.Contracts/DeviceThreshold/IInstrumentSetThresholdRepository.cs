using Entities;
using Models.InstrumentSetThreshold;

namespace Repository.Contracts.DeviceThreshold
{
    public interface IInstrumentSetThresholdRepository
    {
        Task<IEnumerable<ThresholdDeviceEntity>> DeviceInstrumentOnOffNotDelete();
        Task<IEnumerable<ThresholdDeviceEntity>> DeviceInstrumentOnOffDelete();
        Task<IEnumerable<ThresholdDeviceEntity>> DeviceInstrumentOnOffByIdDeviceDriver(Guid Id);
        Task DeviceInstrumentOnOffUpdate(ThresholdDeviceEntity model);
        Task DeviceInstrumentOnOffCreate(ThresholdDeviceEntity model);
        Task<bool> DeviceInstrumentOnOffDeleteById(ThresholdRemoveModel model);
    }
}