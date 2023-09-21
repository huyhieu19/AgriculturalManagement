using Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class StaffConfiguration : IEntityTypeConfiguration<StaffEntity>
    {
        public void Configure(EntityTypeBuilder<StaffEntity> builder)
        {
            builder.ToTable("Staff");

            builder.HasKey(x => x.Id);
        }
    }
}
