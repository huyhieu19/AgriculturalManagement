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
                new InstrumentationTypeEntity(1, "Cảm biến đo nhiệt độ", Unit: "*C"),
                new InstrumentationTypeEntity(2, "Cảm biến đo độ ẩm không khí", Unit: "%"),
                new InstrumentationTypeEntity(3, "Cảm biến nước mưa", Unit: null),
                new InstrumentationTypeEntity(4, "Cảm biên gió", Unit: "Km/h"),
                new InstrumentationTypeEntity(5, "Độ ẩm đất", Unit: "%"),
                new InstrumentationTypeEntity(6, "Cảm biên đo độ PH của đất", Unit: "PH")
                );
        }
    }
}
