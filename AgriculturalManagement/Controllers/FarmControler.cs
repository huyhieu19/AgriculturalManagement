using Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace AgriculturalManagement.Controllers
{
    public class FarmControler : ControllerBase
    {
        private IServiceManager serviceManager;
        public FarmControler(IServiceManager serviceManager) => this.serviceManager = serviceManager;

        [HttpGet, Route("farms")]
        public async Task<FarmEntity> GetFarmAsync()
        {

        }
    }
}
