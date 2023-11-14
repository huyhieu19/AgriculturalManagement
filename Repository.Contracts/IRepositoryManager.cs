using Repository.Contracts.Device;
using Repository.Contracts.DeviceThreshold;
using Repository.Contracts.DeviceTimer;
using Repository.Contracts.Farm;
using Repository.Contracts.Image;

namespace Repository.Contracts
{
    public interface IRepositoryManager
    {
        IFarmRepository Farm { get; }
        IZoneRepository Zone { get; }
        IImageRepository Image { get; }
        IDeviceTimerRepository DeviceDriver { get; }
        IInstrumentSetThresholdRepository InstrumentSetThreshold { get; }
        IInstrumentationTypeRepository InstrumentationType { get; }
        IModuleRepository Module { get; }
        IDeviceRepository Device { get; }
        IMockDataRepository MockData { get; }

        Task<int> SaveAsync();
    }
}