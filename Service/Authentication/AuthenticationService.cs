using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Identity;
using Models.Authentication;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper mapper;
        private readonly UserManager<UserEntity> userManager;

        public AuthenticationService(IMapper mapper, UserManager<UserEntity> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUser(UserRegisterationModel userRegisterationModel)
        {
            var user = mapper.Map<UserEntity>(userRegisterationModel);
            var result = await userManager.CreateAsync(user,
            userRegisterationModel.Password!);
            if (result.Succeeded && userRegisterationModel.Roles != null)
                    await userManager.AddToRolesAsync(user, userRegisterationModel.Roles);
            return result;
        }

    }
}
