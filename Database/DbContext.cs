using Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class DbContext : IdentityDbContext<UserEntity>
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

    }
}