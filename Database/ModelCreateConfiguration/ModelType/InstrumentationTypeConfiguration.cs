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
                new InstrumentationTypeEntity() { Id = new Guid("743b3068-bb94-41f4-bd10-7f4ed802a36c"), Name = "Cảm biến đo nhiệt độ, độ ẩm không khí", Unit = "*C/%" },
                new InstrumentationTypeEntity() { Id = new Guid("d67fb159-c95d-4c67-8f2e-065f14fc5e58"), Name = "Cảm biến đo nhiệt độ, độ ẩm không khí", Unit = "*C/%" },
                new InstrumentationTypeEntity() { Id = new Guid("b6976895-e254-487e-a59c-c22621b2c54a"), Name = "Cảm biến nước mưa", Unit = "true/false" },
                new InstrumentationTypeEntity() { Id = new Guid("1c44a06c-e539-4ead-8c62-fc7d56ec9e34"), Name = "Độ ẩm đất", Unit = "%" },
                new InstrumentationTypeEntity() { Id = new Guid("97f47bef-17cc-45c3-9fbf-69ef9364e12f"), Name = "Cảm biên gió", Unit = "Km/h" },
                new InstrumentationTypeEntity() { Id = new Guid("3057e9c9-b039-489b-be7b-581a751ca4cb"), Name = "Cảm biên đo độ PH của đất", Unit = "PH" }
            );
        }
    }
}