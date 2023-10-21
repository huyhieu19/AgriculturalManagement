using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Contracts;

namespace AgriculturalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IServiceManager service;

        public UserController(IHttpContextAccessor contextAccessor, IServiceManager service)
        {
            _contextAccessor = contextAccessor;
            this.service = service;
        }

        [HttpGet("profile")]
        public async Task<ProfileUser> GetProfile()
        {
            var Id = _contextAccessor.HttpContext!.User.FindFirst("Id")!.Value;
            var profile = await service.User.GetProfile(Id);
            return profile;
        }
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(ProfileUser profile)
        {
            var Id = _contextAccessor.HttpContext!.User.FindFirst("Id")!.Value;
            var Updateprofile = new ProfileUser(Id, UserName: profile.UserName, Email: profile.Email, PhoneNumber: profile.PhoneNumber, Address: profile.Address);
            var result = await service.User.UpdateProfile(Updateprofile);
            if (result.Succeeded)
            {
                return Ok(true);
            }
            return Ok(result);
        }
        [HttpGet("roles")]
        public async Task<List<string>> GetRoles()
        {
            var Id = _contextAccessor.HttpContext!.User.FindFirst("Id")!.Value;
            return await service.User.GetRoles(Id);
        }

    }
}
