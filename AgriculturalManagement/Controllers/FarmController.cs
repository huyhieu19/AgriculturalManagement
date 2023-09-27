using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Contracts;
using System.Security.Claims;

namespace AgriculturalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public FarmController(IServiceManager serviceManager) => this.serviceManager = serviceManager;

        [HttpGet, Route("farms")]
        [Authorize]
        public async Task<IEnumerable<FarmDisplayModel>> GetFarmAsync()
        {
            return await serviceManager.Farm.GetAllFarmAsync(false);
        }
        [HttpPost, Route("farm")]
        public async Task<bool> CreateFarmAsync([FromBody] FarmCreateModel createModel)
        {
            return await serviceManager.Farm.AddFarm(createModel);
        }
        [Authorize]
        [HttpPost, Route("farmsCondition")]
        public async Task<IEnumerable<FarmDisplayModel>> GetCondition([FromBody] QueryBaseModel model)
        {
            //model.Token = GetTokenFromHeader(HttpContext);
            //var id = serviceManager.AuthenticationService.GetIdbyToken(model.Token);

            model.Token = User.FindFirst(ClaimTypes.Name)?.Value;
            model.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await serviceManager.Farm.GetByCondition(model, false);
        }
        [HttpDelete, Route("farm")]
        public async Task<bool> DeleteFarm(int id)
        {
            return await serviceManager.Farm.RemoveFarm(id);
        }
        [HttpPost, Route("farmupdate")]
        public async Task<bool> UpdateFarm([FromBody] FarmUpdateModel model)
        {
            return await serviceManager.Farm.UpdateFarm(model);
        }
        [HttpGet, Route("names")]
        public async Task<IEnumerable<FarmFilterNameModel>> GetNamesFarmAsync()
        {
            return await serviceManager.Farm.GetNameFarm();
        }

        public static string GetTokenFromHeader(HttpContext contex)
        {
            contex.Request.Headers.TryGetValue("Authorization", out var token);
            var result = token.ToString();
            result = result.Substring(6).Trim();
            return result;
        }
    }
}
