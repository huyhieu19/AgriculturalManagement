using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class TimerDeviceDriverConfiguration : IEntityTypeConfiguration<TimerDeviceDriverEntity>
    {
        public void Configure(EntityTypeBuilder<TimerDeviceDriverEntity> builder)
        {
            builder.ToTable("TimerDeviceDriver");

            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.DeviceDriver).WithMany(p => p.TimerDevices).HasForeignKey(t => t.DeviceDriverId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(t => t.IsRemove).HasDefaultValue(false);
            builder.Property(t => t.IsSuccess).HasDefaultValue(false);
            builder.Property(t => t.DeviceDriverId).IsRequired();

        }
    }
}