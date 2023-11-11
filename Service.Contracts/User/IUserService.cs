using Microsoft.AspNetCore.Identity;
using Models;

namespace Service.Contracts
{
    public interface IUserService
    {
        Task<IdentityResult> UpdateProfile(ProfileUser profileUser);
        Task<ProfileUser> GetProfile(string Id);
        Task<List<string>> GetRoles(string Id);
    }
}