
namespace Service.Contracts
{
    public interface IServiceManager
    {
        IFarmService Farm { get; }
        IZoneService Zone { get; }
        IImageService Image { get; }
        IInstrumentationService Instrumentation { get; }
        IDeviceDriverService DeviceDriver { get; }
        IMachineService Machine { get; }
        IAuthenticationService AuthenticationService { get; }
        IValueTypeService ValueType { get; }

        IInstrumentSetThresholdService InstrumentSetThreshold { get; }

        IUserService User { get; }

        IEspService EspService { get; }
    }
}