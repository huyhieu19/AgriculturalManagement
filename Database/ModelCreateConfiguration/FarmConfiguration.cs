using Entities.Farm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class FarmConfiguration : IEntityTypeConfiguration<FarmEntity>
    {
        public void Configure(EntityTypeBuilder<FarmEntity> builder)
        {
            builder.ToTable("Farms");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.User)
                .WithMany(p => p.Farms)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
