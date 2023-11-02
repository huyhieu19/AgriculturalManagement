using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class DeviceInstrumentThresholdConfiguration : IEntityTypeConfiguration<DeviceInstrumentThresholdEntity>
    {
        public void Configure(EntityTypeBuilder<DeviceInstrumentThresholdEntity> builder)
        {
            builder.ToTable("DeviceInstrumentThreshold");
            builder.HasKey(x => new { x.DeviceDriverId, x.InstrumentationId });
            builder.HasOne(p => p.DeviceDriver).WithMany(p => p.DeviceInstrumentOnOffs).HasForeignKey(p => p.DeviceDriverId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(p => p.Instrumentation).WithMany(p => p.DeviceInstrumentOnOffs).HasForeignKey(p => p.InstrumentationId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Property(p => p.OnInUpperThreshold).HasDefaultValue(true);
            builder.Property(p => p.IsDelete).HasDefaultValue(false);
        }
    }
}