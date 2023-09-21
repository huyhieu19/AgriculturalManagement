﻿using Entities;
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

            builder.HasOne(p => p.Staff)
                .WithMany(p => p.Images)
                .HasForeignKey(p => p.StaffId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.User)
                .WithMany(p => p.Images)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.Farm)
                .WithMany(p => p.Images)
                .HasForeignKey(p => p.FarmId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.Zone)
                .WithMany(p => p.Images)
                .HasForeignKey(p => p.ZoneId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}