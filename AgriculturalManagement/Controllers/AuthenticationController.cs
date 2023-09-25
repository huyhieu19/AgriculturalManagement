using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Authentication;
using Service.Contracts;

namespace AgriculturalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthenticationController(IServiceManager service) => _service = service;
        [HttpPost, Route("register")]
        public async Task<IdentityResult> RegisterUser(UserRegisterationModel userRegisterationModel)
        {
            return await _service.AuthenticationService.RegisterUser(userRegisterationModel);
        }
    }
}
