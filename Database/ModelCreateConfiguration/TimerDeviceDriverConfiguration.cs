using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class TimerDeviceDriverConfiguration : IEntityTypeConfiguration<TimerDeviceEntity>
    {
        public void Configure(EntityTypeBuilder<TimerDeviceEntity> builder)
        {
            builder.ToTable("TimerDeviceDriver");

            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Devices).WithMany(p => p.TimerDevices).HasForeignKey(t => t.DeviceId).OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(t => t.IsRemove).HasDefaultValue(false);
            builder.Property(t => t.IsSuccess).HasDefaultValue(false);

        }
    }
}