using AutoMapper;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFarmService> farmService;
        private readonly Lazy<IZoneService> zoneService;

        public ServiceManager(IRepositoryManager repositoryManager,
            ILoggerManager logger,
            IMapper mapper)
        {
            this.farmService = new Lazy<IFarmService>(() => new FarmService(repositoryManager, logger, mapper));
            this.zoneService = new Lazy<IZoneService>(() => new ZoneService(repositoryManager, mapper));
        }


        public IFarmService FarmService => farmService.Value;

        public IZoneService ZoneService => zoneService.Value;
    }
}
