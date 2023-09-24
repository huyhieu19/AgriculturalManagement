using Microsoft.AspNetCore.Identity;

namespace Entities.User
{
    public class UserEntity : IdentityUser
    {
        public string? AvatarUrl { get; set; }
        public ICollection<FarmEntity>? Farms { get; set; }

    }
}
