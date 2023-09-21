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

        public ServiceManager()
        {
            this.farmService = new Lazy<IFarmService>(() => new FarmService());
        }


        public IFarmService FarmService => farmService.Value;
    }
}
