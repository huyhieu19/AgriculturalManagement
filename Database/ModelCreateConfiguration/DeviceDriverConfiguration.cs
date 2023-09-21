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
            builder.HasKey(e => e.Id);
        }
    }
}
