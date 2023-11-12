using Entities.Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    internal class ModuleConfigurarion : IEntityTypeConfiguration<ModuleEntity>
    {
        public void Configure(EntityTypeBuilder<ModuleEntity> builder)
        {
            builder.ToTable("Module");
            builder.Property<Guid>(p => p.Id).IsRequired();
            // config relationship table
            builder.HasOne(p => p.User).WithMany(p => p.Esps).HasForeignKey(p => p.UserId);

            builder.Property(p => p.ClientId).HasDefaultValue("ClientId");
            builder.Property(p => p.MqttServer).HasDefaultValue("broker.emqx.io");
            builder.Property(p => p.MqttPort).HasDefaultValue(1883);
            builder.Property(p => p.UserName).HasDefaultValue("emqx");
            builder.Property(p => p.Password).HasDefaultValue("public");
        }
    }
}