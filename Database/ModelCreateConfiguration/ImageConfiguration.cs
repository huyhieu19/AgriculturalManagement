using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class ImageConfiguration : IEntityTypeConfiguration<ImageEntity>
    {
        public void Configure(EntityTypeBuilder<ImageEntity> builder)
        {
            builder.ToTable("Image");

            builder.HasKey(x => x.Id);

            ////builder.HasOne(p => p.Farms)
            ////    .WithMany(p => p.Images)
            ////    .HasForeignKey(p => p.FarmId)
            ////    .OnDelete(DeleteBehavior.ClientSetNull);

            ////builder.HasOne(p => p.Zone)
            ////    .WithMany(p => p.Images)
            ////    .HasForeignKey(p => p.ZoneId)
            ////    .OnDelete(DeleteBehavior.ClientSetNull);

            ////builder.HasOne(p => p.ZoneHarvest)
            ////    .WithMany(p => p.Images)
            ////    .HasForeignKey(p => p.ZoneHarvestId)
            ////    .OnDelete(DeleteBehavior.ClientSetNull);

            ////builder.HasOne(p => p.Instrumentation)
            ////    .WithMany(p => p.Images)
            ////    .HasForeignKey(p => p.InstrumentationId)
            ////    .OnDelete(DeleteBehavior.ClientSetNull);

            ////builder.HasOne(p => p.DeviceDriver)
            ////    .WithMany(p => p.Images)
            ////    .HasForeignKey(p => p.DeviceDriverId)
            ////    .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
