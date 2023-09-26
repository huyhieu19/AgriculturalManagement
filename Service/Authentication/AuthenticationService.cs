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
        private const int DayRefreshTokenExpiryTime = 7;

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

                var signingCredentials = GetSigningCredentials(); // Tạo chữ kí số
                var claims = await GetClaims(); // Lấy ra những vai trò của user

                var tokenOptions = GenerateTokenOptions(signingCredentials, claims); // Lấy ra JWTSecurityToken thích hợp để tạo Token

                var refreshToken = GenerateRefreshToken();
                _user.RefreshToken = refreshToken;
                if (populateExp)
                    _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(DayRefreshTokenExpiryTime);

                await userManager.UpdateAsync(_user);

                var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions); // sử dụng tokenOptions ở trên và JwtSecurityTokenHandler() để tạo token
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
            /*
             Sử dụng một khóa bí mật và một thuật toán để tạo chữ kí số, Hành động này để kí JWT.
            Khi tạo một JWT, bạn sẽ sử dụng SigningCredentials để ký JWT bằng cách sử dụng khóa bí mật và thuật toán đã xác định.
            Sau đó, khi xác thực JWT, bạn sẽ sử dụng cùng một SigningCredentials để kiểm tra tính toàn vẹn của JWT bằng cách sử dụng khóa tương ứng và thuật toán.
             */
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
            /*
             Phương thức này thường được sử dụng trong quá trình xác thực và ủy quyền để tạo danh sách claims dựa trên thông tin của người dùng.
            Các claims này sau đó có thể được sử dụng để tạo và xác thực token, quyết định quyền truy cập, hoặc hiển thị thông tin người dùng trong ứng dụng.
            */
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
            _logger.LogInfomation("AuthenticationService - GenerateRefreshToken");
            var ramdomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(ramdomNumber);
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
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            return principal;
        }

        public async Task<TokenModel> RefreshToken(TokenModel tokenModel)
        {
            var principal = GetPrincipalFromExpiredToken(tokenModel.AccessToken);

            var user = await userManager.FindByNameAsync(principal.Identity.Name);
            if (user == null || user.RefreshToken != tokenModel.RefreshToken)
            {
                throw new AggregateException("Invalid client request. The tokenDto has some invalid values.");
            }
            _user = user;
            return await CreateToken(populateExp: false);
        }
    }
}
