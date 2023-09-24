using AutoMapper;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFarmService> farmService;
        private readonly Lazy<IZoneService> zoneService;
        private readonly Lazy<IImageService> imageService;

        public ServiceManager(IRepositoryManager repositoryManager,
            ILoggerManager logger,
            IMapper mapper)
        {
            this.farmService = new Lazy<IFarmService>(() => new FarmService(repositoryManager, logger, mapper));
            this.zoneService = new Lazy<IZoneService>(() => new ZoneService(repositoryManager, mapper));
            this.imageService = new Lazy<IImageService>(() => new ImageService(repositoryManager, mapper));
        }


        public IFarmService FarmService => farmService.Value;

        public IZoneService ZoneService => zoneService.Value;

        public IImageService Image => imageService.Value;
    }
}
