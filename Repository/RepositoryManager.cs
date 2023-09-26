using Database;
using Repository.Contracts;
using Service.Contracts;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IFarmRepository> farmRepository;
        private readonly Lazy<IZoneRepository> zoneRepository;
        private readonly Lazy<IImageRepository> imageRepository;
        private readonly Lazy<IInstrumentationRepository> instrumentationRepository;
        private readonly Lazy<IDeviceDriverRepository> deviceDriverRepository;
        private readonly Lazy<IMachineRepository> machineRepository;
        private readonly Lazy<IDeviceDriverTypeRepository> deviceDriverTypeRepository;
        private readonly Lazy<IInstrumentationTypeRepository> instrumentationTypeRepository;
        private readonly Lazy<ITypeTreeRepository> typeTreeRepository;
        private readonly FactDbContext factDbContext;

        public RepositoryManager(FactDbContext factDbContext, ILoggerManager logger, DapperContext dapperContext)
        {
            this.factDbContext = factDbContext;
            farmRepository = new Lazy<IFarmRepository>(() => new FarmRepository(factDbContext, logger, dapperContext));
            zoneRepository = new Lazy<IZoneRepository>(() => new ZoneRepository(factDbContext, dapperContext));
            imageRepository = new Lazy<IImageRepository>(() => new ImageRepository(factDbContext, dapperContext));
            instrumentationRepository = new Lazy<IInstrumentationRepository>(() => new InstrumentationRepository(factDbContext, dapperContext));
            deviceDriverRepository = new Lazy<IDeviceDriverRepository>(() => new DeviceDriverRepository(factDbContext, dapperContext));
            machineRepository = new Lazy<IMachineRepository>(() => new MachineRepository(factDbContext, dapperContext));
            deviceDriverTypeRepository = new Lazy<IDeviceDriverTypeRepository>(() => new DeviceDriverTypeRepository(factDbContext, dapperContext));
            instrumentationTypeRepository = new Lazy<IInstrumentationTypeRepository>(() => new InstrumentationTypeRepository(factDbContext, dapperContext));
            typeTreeRepository = new Lazy<ITypeTreeRepository>(() => new TypeTreeRepository(factDbContext, dapperContext));
        }

        public IFarmRepository Farm => farmRepository.Value;

        public IZoneRepository Zone => zoneRepository.Value;

        public IImageRepository Image => imageRepository.Value;

        public IInstrumentationRepository Instrumentation => instrumentationRepository.Value;

        public IDeviceDriverRepository DeviceDriver => deviceDriverRepository.Value;

        public IMachineRepository Machine => machineRepository.Value;

        public IDeviceDriverTypeRepository DeviceDriverType => deviceDriverTypeRepository.Value;

        public IInstrumentationTypeRepository InstrumentationType => instrumentationTypeRepository.Value;

        public ITypeTreeRepository TypeTree => typeTreeRepository.Value;

        public async Task SaveAsync() => await factDbContext.SaveChangesAsync();
    }
}
