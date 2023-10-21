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
        public string? Address { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<FarmEntity>? Farms { get; set; }
    }
}
