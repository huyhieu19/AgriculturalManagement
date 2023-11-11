using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Farm;
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


        // Create new farm
        [HttpPost, Route("farm")]
        [Authorize(Roles = "Administrator")]
        public async Task<FarmModifyResponseModel> CreateFarmAsync([FromBody] FarmCreateModel createModel)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                throw new ArgumentNullException("Vui lòng đăng nhập vào hệ thống");
            }
            createModel.UserId = userId;
            return await serviceManager.Farm.AddFarm(createModel);
        }
        // Get farms
        [HttpPost, Route("farms")]
        [Authorize(Roles = "Administrator")]
        public async Task<List<FarmDisplayModel>> GetFarms()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                throw new ArgumentNullException("Vui lòng đăng nhập vào hệ thống");
            }
            return await serviceManager.Farm.GetFarms(userId, false);
        }

        // Delete farm
        [HttpDelete, Route("farm")]
        [Authorize(Roles = "Administrator")]
        public async Task<FarmModifyResponseModel> DeleteFarm(int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                throw new ArgumentNullException("Vui lòng đăng nhập vào hệ thống");
            }
            return await serviceManager.Farm.RemoveFarm(id, userId);
        }
        // update farm
        [HttpPost, Route("farm-update")]
        [Authorize(Roles = "Administrator")]
        public async Task<FarmModifyResponseModel> UpdateFarm([FromBody] FarmUpdateModel model)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                throw new ArgumentNullException("Vui lòng đăng nhập vào hệ thống");
            }
            model.UserId = userId;
            return await serviceManager.Farm.UpdateFarm(model);
        }
        private string? GetUserId()
        {
            var id = httpContextAccessor.HttpContext?.User.FindFirstValue("Id");
            return id;
        }
    }
}