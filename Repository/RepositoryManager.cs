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
using Repository.ESP;
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
        private readonly Lazy<IDeviceDriverRepository> deviceDriverRepository;
        private readonly Lazy<IInstrumentationTypeRepository> instrumentationTypeRepository;
        private readonly Lazy<ITypeTreeRepository> typeTreeRepository;
        private readonly Lazy<IInstrumentSetThresholdRepository> instrumentSetThresholdRepository;
        private readonly Lazy<IEspRepository> espRepository;
        private readonly Lazy<IDeviceEspRepository> deviceEspRepository;
        private readonly Lazy<IDeviceRepository> deviceRepository;


        private readonly FactDbContext factDbContext;

        public RepositoryManager(FactDbContext factDbContext, ILoggerManager logger, DapperContext dapperContext, IMapper mapper)
        {
            this.factDbContext = factDbContext;
            farmRepository = new Lazy<IFarmRepository>(() => new FarmRepository(factDbContext, logger));
            zoneRepository = new Lazy<IZoneRepository>(() => new ZoneRepository(factDbContext));
            imageRepository = new Lazy<IImageRepository>(() => new ImageRepository(factDbContext, dapperContext));
            deviceDriverRepository = new Lazy<IDeviceDriverRepository>(() => new DeviceDriverRepository(factDbContext, dapperContext));
            instrumentationTypeRepository = new Lazy<IInstrumentationTypeRepository>(() => new InstrumentationTypeRepository(factDbContext, dapperContext));
            typeTreeRepository = new Lazy<ITypeTreeRepository>(() => new TypeTreeRepository(factDbContext, dapperContext));
            instrumentSetThresholdRepository = new Lazy<IInstrumentSetThresholdRepository>(() => new InstrumentSetThresholdRepository(factDbContext));
            espRepository = new Lazy<IEspRepository>(() => new EspRepository(factDbContext));
            deviceEspRepository = new Lazy<IDeviceEspRepository>(() => new DeviceEspRepository(factDbContext));
            this.deviceRepository = new Lazy<IDeviceRepository>(() => new DeviceRepository(factDbContext));
        }

        public IFarmRepository Farm => farmRepository.Value;

        public IZoneRepository Zone => zoneRepository.Value;

        public IImageRepository Image => imageRepository.Value;

        public IDeviceDriverRepository DeviceDriver => deviceDriverRepository.Value;

        public IInstrumentationTypeRepository InstrumentationType => instrumentationTypeRepository.Value;

        public ITypeTreeRepository TypeTree => typeTreeRepository.Value;

        public IInstrumentSetThresholdRepository InstrumentSetThreshold => instrumentSetThresholdRepository.Value;

        public IEspRepository Esp => espRepository.Value;
        public IDeviceEspRepository DeviceEsp => deviceEspRepository.Value;

        public IDeviceRepository device => deviceRepository.Value;

        public async Task<int> SaveAsync() => await factDbContext.SaveChangesAsync();
    }
}