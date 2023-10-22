using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    internal class EspConfigurarion : IEntityTypeConfiguration<Esp8266Entity>
    {
        public void Configure(EntityTypeBuilder<Esp8266Entity> builder)
        {
            builder.ToTable("Esp");
            builder.Property<Guid>(p => p.Id).IsRequired();
            builder.HasMany(p => p.DeviceDrivers).WithOne(p => p.Esp8266).HasForeignKey(p => p.EspId);
            builder.HasMany(p => p.Instrumentations).WithOne(p => p.Esp8266).HasForeignKey(p => p.EspId);
        }
    }
}
