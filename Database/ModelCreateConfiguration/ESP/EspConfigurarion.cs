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
            builder.Property(p => p.ClientId).HasDefaultValue("ClientId");
            builder.Property(p => p.MqttServer).HasDefaultValue(Guid.NewGuid().ToString());
            builder.Property(p => p.MqttPort).HasDefaultValue(1883);
            builder.Property(p => p.UserName).HasDefaultValue("emqx");
            builder.Property(p => p.Password).HasDefaultValue("public");
            builder.HasMany(p => p.DeviceDrivers).WithOne(p => p.Esp8266).HasForeignKey(p => p.EspId);
            builder.HasMany(p => p.Instrumentations).WithOne(p => p.Esp8266).HasForeignKey(p => p.EspId);

        }
    }
}
