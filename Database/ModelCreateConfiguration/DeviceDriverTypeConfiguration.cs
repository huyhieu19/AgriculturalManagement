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
                new DeviceDriverTypeEntity() { Id = 1, Name = "Máy bơm" },
                new DeviceDriverTypeEntity() { Id = 2, Name = "Quạt gió" },
                new DeviceDriverTypeEntity() { Id = 3, Name = "Rèm cửa" }
                );
        }
    }
}
