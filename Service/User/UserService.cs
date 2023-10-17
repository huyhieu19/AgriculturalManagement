using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Identity;
using Models;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        private readonly UserManager<UserEntity> userManager;

        public UserService(IRepositoryManager repository, IMapper mapper, UserManager<UserEntity> userManager)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<ProfileUser> GetProfile(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            var userProfile = mapper.Map<ProfileUser>(user);
            return userProfile;
        }

        public async Task<IdentityResult> UpdateProfile(ProfileUser profileUser)
        {

            var user = await userManager.FindByIdAsync(profileUser.Id!);
            if (user != null)
            {
                user.PhoneNumber = profileUser.PhoneNumber;
                user.UserName = profileUser.UserName;
                user.Email = profileUser.Email;
                user.Address = profileUser.Address;
            }

            IdentityResult result = await userManager.UpdateAsync(user);
            await repository.SaveAsync();
            return result;
        }
    }
}
