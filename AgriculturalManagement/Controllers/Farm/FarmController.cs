using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Contracts;
using System.Security.Claims;

namespace AgriculturalManagement.Controllers.Farm
{
    [Route("api/Farms")]
    [ApiController]
    public class FarmController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        public FarmController(IServiceManager serviceManager, IHttpContextAccessor httpContextAccessor)
        {
            this.serviceManager = serviceManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet, Route("farms")]
        [Authorize(Roles = "Administrator")]
        public async Task<IEnumerable<FarmDisplayModel>> GetFarmAsync()
        {
            return await serviceManager.Farm.GetAllFarmAsync(false);
        }
        [HttpPost, Route("farm")]
        [Authorize(Roles = "Administrator")]
        public async Task<bool> CreateFarmAsync([FromBody] FarmCreateModel createModel)
        {
            createModel.UserId = httpContextAccessor.HttpContext?.User.FindFirstValue("Id");
            return await serviceManager.Farm.AddFarm(createModel);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost, Route("farms")]
        public async Task<IEnumerable<FarmDisplayModel>> GetFarms()
        {
            string UserId = httpContextAccessor.HttpContext?.User.FindFirstValue("Id")!;
            return await serviceManager.Farm.GetFarms(UserId, false);
        }
        [HttpDelete, Route("farm")]
        [Authorize(Roles = "Administrator")]
        public async Task<bool> DeleteFarm(int id)
        {
            var UserId = httpContextAccessor.HttpContext?.User.FindFirstValue("Id");
            return await serviceManager.Farm.RemoveFarm(id, UserId!);
        }
        [HttpPost, Route("farmupdate")]
        [Authorize(Roles = "Administrator")]
        public async Task<bool> UpdateFarm([FromBody] FarmUpdateModel model)
        {
            var UserId = httpContextAccessor.HttpContext?.User.FindFirstValue("Id");
            model.UserId = UserId;
            return await serviceManager.Farm.UpdateFarm(model);
        }
        [HttpGet, Route("names")]
        [Authorize(Roles = "Administrator")]
        public async Task<IEnumerable<FarmFilterNameModel>> GetNamesFarmAsync()
        {
            string UserId = httpContextAccessor.HttpContext?.User.FindFirstValue("Id")!;
            return await serviceManager.Farm.GetNameFarm(UserId);
        }
    }
}