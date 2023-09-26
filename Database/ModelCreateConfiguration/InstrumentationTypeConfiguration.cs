using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class InstrumentationTypeConfiguration : IEntityTypeConfiguration<InstrumentationTypeEntity>
    {
        public void Configure(EntityTypeBuilder<InstrumentationTypeEntity> builder)
        {
            builder.ToTable("InstrumentationType");
            builder.HasKey(x => x.Id);
            builder.HasData(
                new InstrumentationTypeEntity() { Id = 1, Name = "Cảm biến đo nhiệt độ", Unit = "*C" },
                new InstrumentationTypeEntity() { Id = 2, Name = "Cảm biến đo độ ẩm không khí", Unit = "%" },
                new InstrumentationTypeEntity() { Id = 3, Name = "Cảm biến nước mưa", Unit = null },
                new InstrumentationTypeEntity() { Id = 4, Name = "Cảm biên gió", Unit = "Km/h" },
                new InstrumentationTypeEntity() { Id = 5, Name = "Độ ẩm đất", Unit = "%" },
                new InstrumentationTypeEntity() { Id = 6, Name = "Cảm biên đo độ PH của đất", Unit = "PH" }
                );
        }
    }
}
