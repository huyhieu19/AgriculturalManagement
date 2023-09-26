using Microsoft.AspNetCore.Identity;
using Models;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserRegisterationModel userRegisterationModel);
        Task<bool> ValidateUser(LoginModel userForAuth);// login feature 
        Task<TokenModel> CreateToken(bool populateExp); // login feature

        Task<TokenModel> RefreshToken(TokenModel tokenModel);
    }
}
