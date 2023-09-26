using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class DeviceDriverTypeConfiguration : IEntityTypeConfiguration<DeviceDriverTypeEntity>
    {
        public void Configure(EntityTypeBuilder<DeviceDriverTypeEntity> builder)
        {
            builder.ToTable("DeviceDriverType");
            builder.HasKey(e => e.Id);
            builder.HasData(
                new DeviceDriverTypeEntity(1, "Máy bơm"),
                new DeviceDriverTypeEntity(2, "Quạt gió"),
                new DeviceDriverTypeEntity(3, "Rèm cửa")
                );
        }
    }
}
