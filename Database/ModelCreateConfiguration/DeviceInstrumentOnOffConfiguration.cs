using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class DeviceInstrumentOnOffConfiguration : IEntityTypeConfiguration<DeviceInstrumentOnOffEntity>
    {
        public void Configure(EntityTypeBuilder<DeviceInstrumentOnOffEntity> builder)
        {
            builder.ToTable("DeviceInstrumentOnOff");
            builder.HasKey(x => x.Id);
            builder.HasOne(p => p.DeviceDriver).WithMany(p => p.DeviceInstrumentOnOffs).HasForeignKey(p => p.DeviceDriverId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(p => p.Instrumentation).WithMany(p => p.DeviceInstrumentOnOffs).HasForeignKey(p => p.InstrumentationId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Property(p => p.IsDelete).HasDefaultValue(false);
        }
    }
}
