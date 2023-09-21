using Microsoft.AspNetCore.Identity;

namespace Entities.User
{
    public class UserEntity : IdentityUser
    {
        public ICollection<ImageEntity>? Images { get; set; }
        public ICollection<FarmEntity>? Farms { get; set; }

    }
}
