﻿using AutoMapper;
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

        public ServiceManager(IRepositoryManager repositoryManager,
            ILoggerManager logger,
            IMapper mapper, UserManager<UserEntity> userManager, IConfiguration configuration, DapperContext dapperContext)
        {
            this.farmService = new Lazy<IFarmService>(() => new FarmService(repositoryManager, logger, mapper));
            this.zoneService = new Lazy<IZoneService>(() => new ZoneService(repositoryManager, mapper));
            this.imageService = new Lazy<IImageService>(() => new ImageService(repositoryManager, mapper));
            this.instrumentationService = new Lazy<IInstrumentationService>(() => new InstrumentationService(repositoryManager, mapper));
            this.deviceDriverService = new Lazy<IDeviceDriverService>(() => new DeviceDriverService(repositoryManager, mapper, dapperContext));
            this.machineService = new Lazy<IMachineService>(() => new MachineService(repositoryManager, mapper));
            this.authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(mapper, userManager, logger, configuration));
            this.valueTypeService = new Lazy<IValueTypeService>(() => new ValueTypeService(repositoryManager, logger, mapper));
        }


        public IFarmService Farm => farmService.Value;

        public IZoneService Zone => zoneService.Value;

        public IImageService Image => imageService.Value;

        public IInstrumentationService Instrumentation => instrumentationService.Value;

        public IDeviceDriverService DeviceDriver => deviceDriverService.Value;

        public IMachineService Machine => machineService.Value;

        public IAuthenticationService AuthenticationService => authenticationService.Value;

        public IValueTypeService ValueType => valueTypeService.Value;
    }
}
