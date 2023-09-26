using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Service.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

        public async Task<TokenModel> CreateToken(bool populateExp)
        {
            try
            {
                _logger.LogInfomation("AuthenticationService - CreateToken");
                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaims();
                var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

                var refreshToken = GenerateRefreshToken();
                _user.RefreshToken = refreshToken;
                if (populateExp)
                    _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                await userManager.UpdateAsync(_user);
                var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return new TokenModel(accessToken, refreshToken);
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
                _logger.LogInfomation("AuthenticationService - RegisterUser");
                var user = mapper.Map<UserEntity>(userRegisterationModel);
                var result = await userManager.CreateAsync(user,
                userRegisterationModel.Password!);
                if (result.Succeeded && userRegisterationModel.Roles != null && userRegisterationModel.Roles.Any())
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

        // function create refresh token

        private string GenerateRefreshToken()
        {
            var ramdomNumber = new byte[32];
            using(var rng = RandomNumberGenerator.Create())
            {
                return Convert.ToBase64String(ramdomNumber);
            }

        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            // TokenValidationParameters to validate the authenticity of JWT (JSON Web Token) during authentication. Here's an explanation of each property in TokenValidationParameters
            var tokenValidationParameters = new TokenValidationParameters
            {
                
                ValidateAudience = true, // ValidateAudience: This property determines whether the token should be validated for its audience (the entity using the token). If you set it to true, the token will be checked to ensure that its audience matches the value specified in ValidAudience.
                
                ValidateIssuer = true, // ValidateIssuer: This property determines whether the token should be validated for its issuer (the entity that issued the token). If you set it to true, the token will be checked to ensure that its issuer matches the value specified in ValidIssuer.
                
                ValidateIssuerSigningKey = true, // ValidateIssuerSigningKey: This property determines whether the token should be validated for the signing key of the issuer. If you set it to true, the token will be checked to ensure that the signing key you provide in IssuerSigningKey matches the signing key the issuer used to sign the token.
                
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("SECRETVariable").ToString()!)), //IssuerSigningKey: This is the signing key used to verify the token's signature. In your case, you are retrieving the value of the signing key from the environment variable named "SECRETVariable" as configured in _configuration.
                
                ValidateLifetime = true, // TokenValidationParameters to validate the authenticity of JWT (JSON Web Token) during authentication. Here's an explanation of each property in TokenValidationParameters
                
                ValidIssuer = jwtSettings["validIssuer"], // ValidIssuer: This is the valid issuer value you specify. The token will be checked to ensure that its issuer matches this value.
                
                ValidAudience = jwtSettings["validAudience"] // ValidAudience: This is the valid audience value you specify. The token will be checked to ensure that its audience matches this value.
            };

            var tokenHander = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHander.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if(jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            return principal;
        }
    }
}
