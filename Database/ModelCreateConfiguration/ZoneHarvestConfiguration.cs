using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class ZoneHarvestConfiguration : IEntityTypeConfiguration<ZoneHarvestEntity>
    {
        public void Configure(EntityTypeBuilder<ZoneHarvestEntity> builder)
        {
            builder.ToTable("ZoneHarvest");

            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.Zone).WithMany(p => p.Harvests).HasForeignKey(p => p.ZoneId);
        }
    }
}
