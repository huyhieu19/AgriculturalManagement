using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class DeviceDriverConfiguration : IEntityTypeConfiguration<DeviceDriverEntity>
    {
        public void Configure(EntityTypeBuilder<DeviceDriverEntity> builder)
        {
            builder.ToTable("DeviceDriver");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.IsAction).HasDefaultValue(false);
            builder.Property(p => p.IsAuto).HasDefaultValue(false);

            builder.Property(p => p.Topic).HasDefaultValue("D");

            builder.HasOne(p => p.Zone)
                .WithMany(p => p.ZoneDeviceDrivers)
                .HasForeignKey(p => p.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}