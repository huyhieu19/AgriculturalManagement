using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class ZoneDeviceDriverConfiguration : IEntityTypeConfiguration<ZoneDeviceDriverEntity>
    {
        public void Configure(EntityTypeBuilder<ZoneDeviceDriverEntity> builder)
        {
            builder.ToTable("ZoneDeviceDriver");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.DeviceDriver)
                .WithMany(p => p.ZoneDeviceDrivers)
                .HasForeignKey(p => p.DeviceDriverId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(p => p.Zone)
                .WithMany(p => p.ZoneDeviceDrivers)
                .HasForeignKey(p => p.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
