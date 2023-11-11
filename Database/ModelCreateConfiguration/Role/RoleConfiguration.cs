using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration.Role
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole
            {
                Id = "27cff7a9-5dd2-4300-9185-d7be99c4da16",
                Name = "Manager",
                NormalizedName = "MANAGER"
            },
            new IdentityRole
            {
                Id = "efd37bbc-5fa6-45ba-a749-bf93cdadbf60",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            });
        }
    }
}