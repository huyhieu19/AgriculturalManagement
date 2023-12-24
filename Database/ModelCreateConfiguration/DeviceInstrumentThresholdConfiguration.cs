using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class DeviceInstrumentThresholdConfiguration : IEntityTypeConfiguration<ThresholdDeviceEntity>
    {
        public void Configure(EntityTypeBuilder<ThresholdDeviceEntity> builder)
        {
            builder.ToTable("DeviceInstrumentThreshold");
            builder.HasKey(x => new { x.DeviceDriverId, x.InstrumentationId, x.TypeDevice });

            builder.HasOne(p => p.DeviceDriver).WithMany().HasForeignKey(p => p.DeviceDriverId).OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(p => p.DeviceInstrumentation).WithMany().HasForeignKey(p => p.InstrumentationId).OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(p => p.OnInUpperThreshold).HasDefaultValue(true);
            builder.Property(p => p.IsDelete).HasDefaultValue(false);
        }
    }
}