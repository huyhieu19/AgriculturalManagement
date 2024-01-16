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
            _logger.LogInformation(message: "Authentication - Register", new LogProcessModel()
            {
                LoggerProcessType = LoggerProcessType.Authentication,
                LogMessageDetail = "Register",
                ServiceName = null,
                User = userRegisterationModel.Email
            });
            var result = await _service.Authentication.RegisterUser(userRegisterationModel);
            if (!result.Succeeded)
            {
                throw new AggregateException(result.ToString());
            }
            return result;
        }
        [HttpPost("login")]
        public async Task<LoginResModel> Authenticate([FromBody] LoginModel loginModel)
        {
            _logger.LogInformation(message: "AuthenticationService - Login", new LogProcessModel()
            {
                LoggerProcessType = LoggerProcessType.Authentication,
                LogMessageDetail = "Login",
                ServiceName = null,
                User = loginModel.Email
            });
            if (!await _service.Authentication.ValidateUser(loginModel))
                return new LoginResModel(IsSuccessed: false);

            var tokenModel = await _service.Authentication.CreateToken(populateExp: true);
            var profile = _service.Authentication.GetProfilebyToken(tokenModel.AccessToken);

            return new LoginResModel(Profile: profile, Token: tokenModel);
        }
        [HttpGet("roles")]
        public async Task<List<IdentityRole>> GetRoles() => await _service.Authentication.GetRoles();
        [HttpPost("role-add-to-user")]
        public async Task<bool> AddRoleToUser(string roleName, string email)
        {
            _logger.LogInformation(message: "Add Role To User", new LogProcessModel()
            {
                LoggerProcessType = LoggerProcessType.Authentication,
                LogMessageDetail = $"{roleName} - {email}",
                ServiceName = nameof(AddRoleToUser),
                User = email
            });
            return await _service.Authentication.AddRoleToUser(roleName, email);
        }

        [HttpPost("password-reset")]
        public async Task<ResponseResetPasswordModel> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            return await _service.Authentication.ChangePassword(resetPasswordModel);
        }
    }
}