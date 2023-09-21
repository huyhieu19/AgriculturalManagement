using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class ZoneConfiguration : IEntityTypeConfiguration<ZoneEntity>
    {
        public void Configure(EntityTypeBuilder<ZoneEntity> builder)
        {
            builder.ToTable("Zone");

            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.Farm)
                .WithMany(p => p.Zones)
                .HasForeignKey(p => p.FarmId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(p => p.Type)
                .WithMany(p => p.Zones)
                .HasForeignKey(p => p.TypeTreeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
