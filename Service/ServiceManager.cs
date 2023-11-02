using AutoMapper;
using Database;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFarmService> farmService;
        private readonly Lazy<IZoneService> zoneService;
        private readonly Lazy<IImageService> imageService;
        private readonly Lazy<IInstrumentationService> instrumentationService;
        private readonly Lazy<IDeviceDriverService> deviceDriverService;
        private readonly Lazy<IMachineService> machineService;
        private readonly Lazy<IAuthenticationService> authenticationService;
        private readonly Lazy<IValueTypeService> valueTypeService;
        private readonly Lazy<IInstrumentSetThresholdService> instrumentSetThresholdService;
        private readonly Lazy<IUserService> userService;
        private readonly Lazy<IEspService> espService;

        public ServiceManager(IRepositoryManager repositoryManager,
            ILoggerManager logger,
            IMapper mapper, UserManager<UserEntity> userManager, IConfiguration configuration, DapperContext dapperContext, FactDbContext factDbContext)
        {
            this.farmService = new Lazy<IFarmService>(() => new FarmService(repositoryManager, logger, mapper, dapperContext));
            this.zoneService = new Lazy<IZoneService>(() => new ZoneService(repositoryManager, mapper, dapperContext));
            this.imageService = new Lazy<IImageService>(() => new ImageService(repositoryManager, mapper));
            this.instrumentationService = new Lazy<IInstrumentationService>(() => new InstrumentationService(repositoryManager, mapper));
            this.deviceDriverService = new Lazy<IDeviceDriverService>(() => new DeviceDriverService(repositoryManager, mapper, dapperContext, logger));
            this.machineService = new Lazy<IMachineService>(() => new MachineService(repositoryManager, mapper));
            this.authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(mapper, factDbContext, userManager, logger, configuration));
            this.valueTypeService = new Lazy<IValueTypeService>(() => new ValueTypeService(repositoryManager, logger, mapper));
            this.instrumentSetThresholdService = new Lazy<IInstrumentSetThresholdService>(() => new InstrumentSetThresholdService(repositoryManager, mapper));
            this.userService = new Lazy<IUserService>(() => new UserService(repositoryManager, mapper, userManager));
            this.espService = new Lazy<IEspService>(() => new EspService(repositoryManager, mapper));
        }


        public IFarmService Farm => farmService.Value;

        public IZoneService Zone => zoneService.Value;

        public IImageService Image => imageService.Value;

        public IInstrumentationService Instrumentation => instrumentationService.Value;

        public IDeviceDriverService DeviceDriver => deviceDriverService.Value;

        public IMachineService Machine => machineService.Value;

        public IAuthenticationService AuthenticationService => authenticationService.Value;

        public IValueTypeService ValueType => valueTypeService.Value;

        public IInstrumentSetThresholdService InstrumentSetThreshold => instrumentSetThresholdService.Value;

        public IUserService User => userService.Value;

        public IEspService EspService => espService.Value;
    }
}