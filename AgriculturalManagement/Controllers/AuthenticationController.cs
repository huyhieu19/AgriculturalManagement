using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
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
            var result = await _service.AuthenticationService.RegisterUser(userRegisterationModel);
            if (!result.Succeeded)
            {
                throw new AggregateException(result.ToString());
            }
            return result;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel user)
        {
            if (!await _service.AuthenticationService.ValidateUser(user))
                return Unauthorized();
            return Ok(new
            {
                Token = await _service
            .AuthenticationService.CreateToken()
            });
        }

        //"firstName": "Hieu",
        //"lastName": "Nguyen Huy",
        //"userName": "HuyHieu",
        //"password": "String123",
        //"email": "Hieu@gmail.com",
        //"phoneNumber": "string",
        //"roles": null
    }
}
