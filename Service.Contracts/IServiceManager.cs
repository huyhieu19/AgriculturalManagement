
using Service.Contracts.Device;
using Service.Contracts.DeviceThreshold;
using Service.Contracts.DeviceTimer;
using Service.Contracts.FarmZone;
using Service.Contracts.Image;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        IFarmService Farm { get; }
        IZoneService Zone { get; }
        IImageService Image { get; }
        IInstrumentationService Instrumentation { get; }
        IDeviceTimerService DeviceDriver { get; }
        IAuthenticationService AuthenticationService { get; }
        IValueTypeService ValueType { get; }
        IInstrumentSetThresholdService InstrumentSetThreshold { get; }
        IUserService User { get; }
        IEspService EspService { get; }
        IDeviceService Device { get; }
    }
}