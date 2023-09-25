using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Service.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper mapper;
        private readonly UserManager<UserEntity> userManager;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private UserEntity? _user;

        public AuthenticationService(IMapper mapper, UserManager<UserEntity> userManager, ILoggerManager _logger, IConfiguration _configuration)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this._logger = _logger;
            this._configuration = _configuration;
        }

        public async Task<string> CreateToken()
        {
            try
            {
                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaims();
                var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
                return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            }
            catch (Exception ex)
            {
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<IdentityResult> RegisterUser(UserRegisterationModel userRegisterationModel)
        {

            try
            {
                var user = mapper.Map<UserEntity>(userRegisterationModel);
                var result = await userManager.CreateAsync(user,
                userRegisterationModel.Password!);
                if (result.Succeeded && userRegisterationModel.Roles != null)
                    await userManager.AddToRolesAsync(user, userRegisterationModel.Roles);
                return result;
            }
            catch (Exception ex)
            {
                throw new AggregateException(ex.Message);
            }
        }
        public async Task<bool> ValidateUser(LoginModel userForAuth)
        {
            _user = await userManager.FindByNameAsync(userForAuth.UserName!);
            var result = (_user != null && await userManager.CheckPasswordAsync(_user,
            userForAuth.Password!));
            if (!result)
                _logger.LogWarning($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password.");
            return result;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("SECRETVariable").ToString()!);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, _user.UserName)
                };
            var roles = await userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
