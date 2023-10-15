using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Contracts;

namespace AgriculturalManagement.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthenticationController(IServiceManager service, IHttpContextAccessor contextAccessor)
        {
            _service = service;
            _contextAccessor = contextAccessor;
        }
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

            var tokenModel = await _service.AuthenticationService.CreateToken(populateExp: true);
            var profile = _service.AuthenticationService.GetProfilebyToken(tokenModel.AccessToken);

            return Ok(new { profile, tokenModel });
        }


        //"firstName": "Hieu",
        //"lastName": "Nguyen Huy",
        //"userName": "HuyHieu",
        //"password": "String123",
        //"email": "Hieu@gmail.com",
        //"phoneNumber": "string",
        //"roles": null

        //"firstName": "Hieu",
        //"lastName": "Nguyen Huy",
        //"userName": "huyhieu",
        //"password": "String123",
        //"email": "Hieu@gmail.com",
        //"phoneNumber": "string",
        //"roles": null
    }
}
