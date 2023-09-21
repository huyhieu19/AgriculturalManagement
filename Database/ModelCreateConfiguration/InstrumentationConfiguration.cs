﻿using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class InstrumentationConfiguration : IEntityTypeConfiguration<InstrumentationEntity>
    {
        public void Configure(EntityTypeBuilder<InstrumentationEntity> builder)
        {
            builder.ToTable("Instrumentation");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Zone)
                .WithMany(p => p.Instrumentations)
                .HasForeignKey(p => p.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull);


        }
    }
}