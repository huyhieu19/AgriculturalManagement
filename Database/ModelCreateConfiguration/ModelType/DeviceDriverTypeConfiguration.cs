using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class DeviceDriverTypeConfiguration : IEntityTypeConfiguration<DeviceDriverTypeEntity>
    {
        public void Configure(EntityTypeBuilder<DeviceDriverTypeEntity> builder)
        {
            builder.ToTable("DeviceDriverType");
            builder.HasKey(e => e.Id);
            builder.HasData(
                new DeviceDriverTypeEntity() { Id = new Guid("448baf97-9401-4aaa-a636-9d8512d7c5a4"), Name = "Máy bơm" },
                new DeviceDriverTypeEntity() { Id = new Guid("add310fe-34e9-4b07-8d66-38a16bc2b177"), Name = "Máy bơm" },
                new DeviceDriverTypeEntity() { Id = new Guid("0f0ae0bc-0454-42db-8c21-338a69448925"), Name = "Quạt gió" },
                new DeviceDriverTypeEntity() { Id = new Guid("032d4594-88fd-43af-bb83-9ea4351ed488"), Name = "Quạt gió" },
                new DeviceDriverTypeEntity() { Id = new Guid("134d6c68-d44c-4bad-b1e5-8e30aadf2c53"), Name = "Rèm cửa" }
            );
        }
    }
}