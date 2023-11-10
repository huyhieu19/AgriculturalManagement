using Entities;
using Entities.ESP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration;

public class DeviceTypeOnEspConfiguration : IEntityTypeConfiguration<DeviceTypeEspEntity>
{
    public void Configure(EntityTypeBuilder<DeviceTypeEspEntity> builder)
    {
        builder.ToTable("DeviceType");

        builder.HasKey(x => x.Id);
        builder.Property(p => p.IsUsed).HasDefaultValue(false);
        builder.HasOne(p => p.Esp).WithMany(p => p.DeviceTypes).HasForeignKey(p => p.EspId);
        builder.HasOne(p => p.DeviceDriver).WithOne(p => p.DeviceType).HasForeignKey<DeviceDriverEntity>(p => p.DeviceTypeId);
        builder.HasOne(p => p.Instrumentation).WithOne(p => p.DeviceType).HasForeignKey<InstrumentationEntity>(p => p.DeviceTypeId);
    }
}