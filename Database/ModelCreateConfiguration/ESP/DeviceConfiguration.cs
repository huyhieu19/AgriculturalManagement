using Entities.ESP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration;

public class DeviceConfiguration : IEntityTypeConfiguration<DeviceEntity>
{
    public void Configure(EntityTypeBuilder<DeviceEntity> builder)
    {
        builder.ToTable("Device");

        builder.HasKey(x => x.Id);
        builder.Property(p => p.IsUsed).HasDefaultValue(false);

        builder.HasOne(p => p.Zone).WithMany(p => p.Devices).HasForeignKey(p => p.ZoneId);

        builder.HasOne(p => p.Esp).WithMany(p => p.DeviceTypes).HasForeignKey(p => p.EspId);
    }
}