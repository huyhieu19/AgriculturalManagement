using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class MachineWarranlyDateConfiguration : IEntityTypeConfiguration<MachineWarranlyDateEntity>
    {
        public void Configure(EntityTypeBuilder<MachineWarranlyDateEntity> builder)
        {
            builder.ToTable("MachineWarranlyDate");
            builder.HasKey(e => e.Id);

            builder.HasOne(p => p.Machine)
                .WithMany(p => p.MachineWarranlyDates)
                .HasForeignKey(p => p.MachineId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(p => p.Instrumentation)
                .WithMany(p => p.MachineWarranlyDates)
                .HasForeignKey(p => p.InstrumentationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(p => p.DeviceDriver)
                .WithMany(p => p.MachineWarranlyDates)
                .HasForeignKey(p => p.DeviceDriversId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
