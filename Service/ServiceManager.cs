using AutoMapper;
using Database;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repository.Contracts;
using Service.Contracts;
using Service.Contracts.Device;
using Service.Contracts.DeviceThreshold;
using Service.Contracts.DeviceTimer;
using Service.Contracts.FarmZone;
using Service.Contracts.Image;
using Service.Contracts.Logger;
using Service.Device;
using Service.DeviceThreshold;
using Service.DeviceTimer;
using Service.Farm;
using Service.Image;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFarmService> farm;
        private readonly Lazy<IZoneService> zone;
        private readonly Lazy<IImageService> image;
        private readonly Lazy<IDeviceTimerService> deviceDriver;
        private readonly Lazy<IAuthenticationService> authentication;
        private readonly Lazy<IValueTypeService> valueType;
        private readonly Lazy<IInstrumentSetThresholdService> instrumentSetThreshold;
        private readonly Lazy<IUserService> user;
        private readonly Lazy<IModuleService> esp;
        private readonly Lazy<IDeviceService> device;
        private readonly Lazy<IMockDataService> mockData;

        public ServiceManager(IRepositoryManager repositoryManager,
            ILoggerManager logger,
            IMapper mapper, UserManager<UserEntity> userManager, IConfiguration configuration, DapperContext dapperContext, FactDbContext factDbContext)
        {
            this.farm = new Lazy<IFarmService>(() => new FarmService(repositoryManager, logger, mapper));
            this.zone = new Lazy<IZoneService>(() => new ZoneService(repositoryManager, mapper));
            this.image = new Lazy<IImageService>(() => new ImageService(repositoryManager, mapper));
            this.deviceDriver = new Lazy<IDeviceTimerService>(() => new DeviceTimerService(repositoryManager, mapper, dapperContext, logger));
            this.authentication = new Lazy<IAuthenticationService>(() => new AuthenticationService(mapper, factDbContext, userManager, logger, configuration));
            this.valueType = new Lazy<IValueTypeService>(() => new ValueTypeService(repositoryManager, logger, mapper));
            this.instrumentSetThreshold = new Lazy<IInstrumentSetThresholdService>(() => new InstrumentSetThresholdService(repositoryManager, mapper, dapperContext, logger));
            this.user = new Lazy<IUserService>(() => new UserService(repositoryManager, mapper, userManager));
            this.esp = new Lazy<IModuleService>(() => new ModuleService(repositoryManager, mapper));
            this.device = new Lazy<IDeviceService>(() => new DeviceService(repositoryManager, mapper, dapperContext));
            this.mockData = new Lazy<IMockDataService>(() => new MockDataService(repositoryManager, mapper));
        }


        public IFarmService Farm => farm.Value;

        public IZoneService Zone => zone.Value;

        public IImageService Image => image.Value;

        public IDeviceTimerService DeviceTimer => deviceDriver.Value;

        public IAuthenticationService Authentication => authentication.Value;

        public IValueTypeService ValueType => valueType.Value;

        public IInstrumentSetThresholdService InstrumentSetThreshold => instrumentSetThreshold.Value;

        public IUserService User => user.Value;

        public IModuleService Module => esp.Value;

        public IDeviceService Device => device.Value;

        public IMockDataService MockData => mockData.Value;

    }
}