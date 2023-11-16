using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class JobInZoneConfiguration : IEntityTypeConfiguration<JobInZoneEntity>
    {
        public void Configure(EntityTypeBuilder<JobInZoneEntity> builder)
        {
            builder.ToTable("JobInZone");
            builder.HasKey(e => e.Id);

            builder.HasOne(p => p.Zone).WithMany(p => p.JobInZones).HasForeignKey(p => p.Id);
        }
    }
}