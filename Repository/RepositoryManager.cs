using AutoMapper;
using Database;
using Repository.Contracts;
using Repository.Contracts.Device;
using Repository.Contracts.DeviceThreshold;
using Repository.Contracts.DeviceTimer;
using Repository.Contracts.Farm;
using Repository.Contracts.Image;
using Repository.Device;
using Repository.DeviceThreshold;
using Repository.DeviceTimer;
using Repository.FarmZone;
using Repository.Image;
using Service.Contracts.Logger;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IFarmRepository> farmRepository;
        private readonly Lazy<IZoneRepository> zoneRepository;
        private readonly Lazy<IImageRepository> imageRepository;
        private readonly Lazy<IDeviceTimerRepository> deviceDriverRepository;
        private readonly Lazy<IInstrumentationTypeRepository> instrumentationTypeRepository;
        private readonly Lazy<ITypeTreeRepository> typeTreeRepository;
        private readonly Lazy<IInstrumentSetThresholdRepository> instrumentSetThresholdRepository;
        private readonly Lazy<IModuleRepository> espRepository;
        private readonly Lazy<IDeviceRepository> deviceRepository;
        private readonly Lazy<IMockDataRepository> mockDataRepository;


        private readonly FactDbContext factDbContext;

        public RepositoryManager(FactDbContext factDbContext, ILoggerManager logger, DapperContext dapperContext, IMapper mapper)
        {
            this.factDbContext = factDbContext;
            farmRepository = new Lazy<IFarmRepository>(() => new FarmRepository(factDbContext, logger));
            zoneRepository = new Lazy<IZoneRepository>(() => new ZoneRepository(factDbContext));
            imageRepository = new Lazy<IImageRepository>(() => new ImageRepository(factDbContext, dapperContext));
            deviceDriverRepository = new Lazy<IDeviceTimerRepository>(() => new DeviceTimerRepository(factDbContext));
            instrumentationTypeRepository = new Lazy<IInstrumentationTypeRepository>(() => new InstrumentationTypeRepository(factDbContext, dapperContext));
            typeTreeRepository = new Lazy<ITypeTreeRepository>(() => new TypeTreeRepository(factDbContext, dapperContext));
            instrumentSetThresholdRepository = new Lazy<IInstrumentSetThresholdRepository>(() => new InstrumentSetThresholdRepository(factDbContext));
            espRepository = new Lazy<IModuleRepository>(() => new ModuleRepository(factDbContext));
            this.deviceRepository = new Lazy<IDeviceRepository>(() => new DeviceRepository(factDbContext));
            this.mockDataRepository = new Lazy<IMockDataRepository>(() => new MockDataRepository(factDbContext));
        }

        public IFarmRepository Farm => farmRepository.Value;

        public IZoneRepository Zone => zoneRepository.Value;

        public IImageRepository Image => imageRepository.Value;

        public IDeviceTimerRepository DeviceDriver => deviceDriverRepository.Value;

        public IInstrumentationTypeRepository InstrumentationType => instrumentationTypeRepository.Value;

        public ITypeTreeRepository TypeTree => typeTreeRepository.Value;

        public IInstrumentSetThresholdRepository InstrumentSetThreshold => instrumentSetThresholdRepository.Value;

        public IModuleRepository Module => espRepository.Value;

        public IDeviceRepository Device => deviceRepository.Value;

        public IMockDataRepository mockData => mockDataRepository.Value;

        public async Task<int> SaveAsync() => await factDbContext.SaveChangesAsync();
    }
}