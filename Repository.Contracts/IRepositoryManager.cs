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

        #region Value Type Repository
        IDeviceDriverTypeRepository DeviceDriverType { get; }
        IInstrumentationTypeRepository InstrumentationType { get; }
        ITypeTreeRepository TypeTree { get; }
        #endregion
        Task SaveAsync();
    }
}
