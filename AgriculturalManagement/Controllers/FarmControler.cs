﻿using Microsoft.AspNetCore.Mvc;
using Models;
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
        [HttpPost, Route("farmsCondition")]
        public async Task<IEnumerable<FarmDisplayModel>> GetCondition([FromBody] QueryBaseModel model)
        {
            return await serviceManager.FarmService.GetByCondition(model, false);
        }
        [HttpDelete, Route("farm")]
        public async Task<bool> DeleteFarm(int id)
        {
            return await serviceManager.FarmService.RemoveFarm(id);
        }
        [HttpPost, Route("farmupdate")]
        public async Task<bool> UpdateFarm([FromBody] FarmUpdateModel model)
        {
            return await serviceManager.FarmService.UpdateFarm(model);
        }
    }
}
