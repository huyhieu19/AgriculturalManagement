using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.ModelCreateConfiguration
{
    public class InstrumentationTypeConfiguration : IEntityTypeConfiguration<InstrumentationTypeEntity>
    {
        public void Configure(EntityTypeBuilder<InstrumentationTypeEntity> builder)
        {
            builder.ToTable("InstrumentationType");
            builder.HasKey(x => x.Id);
            
        }
    }
}
