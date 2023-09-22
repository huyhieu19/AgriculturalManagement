using Entities;
using Microsoft.AspNetCore.Mvc;
using Models.Farm;
using Service.Contracts;

namespace AgriculturalManagement.Controllers
{
    public class FarmControler : ControllerBase
    {
        private IServiceManager serviceManager;
        public FarmControler(IServiceManager serviceManager) => this.serviceManager = serviceManager;

        [HttpGet, Route("farms")]
        public async Task<IEnumerable<FarmDisplayModel>> GetFarmAsync()
        {
            return await serviceManager.FarmService.GetAllFarmAsync(false);
        }
        [HttpPost, Route("farm")]
        public async Task<bool> CreateFarmAsync([FromBody] FarmCreateModel createModel)
        {
            return await serviceManager.FarmService.AddFarm(createModel);
        }
    }
}
