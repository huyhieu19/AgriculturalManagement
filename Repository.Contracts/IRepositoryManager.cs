using Repository.Contracts.Device;

namespace Repository.Contracts
{
    public interface IRepositoryManager
    {
        IFarmRepository Farm { get; }
        IZoneRepository Zone { get; }
        IImageRepository Image { get; }
        IDeviceDriverRepository DeviceDriver { get; }

        IInstrumentSetThresholdRepository InstrumentSetThreshold { get; }


        #region Value Type Repository
        IInstrumentationTypeRepository InstrumentationType { get; }
        ITypeTreeRepository TypeTree { get; }
        #endregion

        IEspRepository Esp { get; }
        IDeviceEspRepository DeviceEsp { get; }

        IDeviceRepository device { get; }

        Task<int> SaveAsync();
    }
}