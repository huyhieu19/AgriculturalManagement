using Microsoft.AspNetCore.Identity;
using Models;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserRegisterationModel userRegisterationModel);
        Task<bool> ValidateUser(LoginModel userForAuth);
        Task<string> CreateToken();
    }
}
