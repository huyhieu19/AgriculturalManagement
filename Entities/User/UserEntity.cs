using Microsoft.AspNetCore.Identity;


//IdentityUser
//IdentityRole
//IdentityUserClaim
//IdentityUserToken
//IdentityUserLogin
//IdentityRoleClaim
//IdentityUserRole

namespace Entities
{
    public class UserEntity : IdentityUser
    {
        public string? AvatarUrl { get; set; }
        public ICollection<FarmEntity>? Farms { get; set; }

    }
}
