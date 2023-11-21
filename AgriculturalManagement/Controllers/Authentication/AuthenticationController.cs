using Common.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Authentication;
using Service.Contracts;
using Service.Contracts.Logger;

namespace AgriculturalManagement.Controllers.Authentication
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly ILoggerManager _logger;
        public AuthenticationController(IServiceManager service, ILoggerManager _logger)
        {
            _service = service;
            this._logger = _logger;
        }
        [HttpPost, Route("register")]
        public async Task<IdentityResult> RegisterUser(UserRegisterationModel userRegisterationModel)
        {
            _logger.LogInformation(message: "Authentication", null, ProcessType: LoggerProcessType.Authentication, "Login", userRegisterationModel.Email);
            var result = await _service.Authentication.RegisterUser(userRegisterationModel);
            if (!result.Succeeded)
            {
                throw new AggregateException(result.ToString());
            }
            return result;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel user)
        {
            _logger.LogInformation("AuthenticationService - Login", "Authentication Service", LoggerProcessType.Authentication, "Login", user.Email);
            if (!await _service.Authentication.ValidateUser(user))
                return BadRequest("Email or Password incorrect");

            var tokenModel = await _service.Authentication.CreateToken(populateExp: true);
            var profile = _service.Authentication.GetProfilebyToken(tokenModel.AccessToken);

            return Ok(new { profile, tokenModel });
        }
        [HttpGet("roles")]
        public async Task<List<IdentityRole>> GetRoles() => await _service.Authentication.GetRoles();
        [HttpPost("role-add-to-user")]
        public async Task<bool> AddRoleToUser(string roleName, string email) => await _service.Authentication.AddRoleToUser(roleName, email);

        [HttpPost("password-reset")]
        public async Task<ResponseResetPasswordModel> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            return await _service.Authentication.ChangePassword(resetPasswordModel);
        }
    }
}