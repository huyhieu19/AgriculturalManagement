namespace Repository.Contracts
{
    public interface IRepositoryManager
    {
        IFarmRepository Farm { get; }
        IZoneRepository Zone { get; }
        IImageRepository Image { get; }
        IInstrumentationRepository Instrumentation { get; }
        IDeviceDriverRepository DeviceDriver { get; }
        IMachineRepository Machine { get; }
        Task SaveAsync();
    }
}
