using AutoMapper;
using Repository.Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFarmService> farmService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            this.farmService = new Lazy<IFarmService>(() => new FarmService(repositoryManager, logger, mapper));
        }


        public IFarmService FarmService => farmService.Value;
    }
}
