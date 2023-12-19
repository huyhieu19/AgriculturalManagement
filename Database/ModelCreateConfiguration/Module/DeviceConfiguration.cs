using Common.Enum;
using Entities.Module;
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
        builder.Property(p => p.TypeStatis).HasDefaultValue(StatisticType.ValueDouble);
        builder.HasOne(p => p.Zone).WithMany(p => p.Devices).HasForeignKey(p => p.ZoneId);

        builder.HasOne(p => p.Module).WithMany(p => p.Devices).HasForeignKey(p => p.ModuleId);
    }
}