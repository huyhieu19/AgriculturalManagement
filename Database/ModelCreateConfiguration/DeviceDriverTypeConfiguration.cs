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
        }
    }
}
