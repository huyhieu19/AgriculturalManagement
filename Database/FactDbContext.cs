using Database.ModelCreateConfiguration;
using Entities;
using Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class FactDbContext : IdentityDbContext<UserEntity>
    {
        public FactDbContext(DbContextOptions<FactDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Method intentionally left empty.
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Bỏ tiền tố AspNet của các bảng: mặc định các bảng trong IdentityDbContext có
            // tên với tiền tố AspNet như: AspNetUserRoles, AspNetUser ...
            // Đoạn mã sau chạy khi khởi tạo DbContext, tạo database sẽ loại bỏ tiền tố đó
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName!.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            // Image configuration
            builder.ApplyConfiguration(new ImageConfiguration());

            //Farm configuration
            builder.ApplyConfiguration(new FarmConfiguration());

            builder.ApplyConfiguration(new InstrumentationConfiguration());
            builder.ApplyConfiguration(new MachineWarranlyDateConfiguration());
            builder.ApplyConfiguration(new ZoneConfiguration());
            builder.ApplyConfiguration(new ZoneDeviceDriverConfiguration());
            builder.ApplyConfiguration(new DeviceDriverConfiguration());
            builder.ApplyConfiguration(new TypeTreeConfiguration());
            builder.ApplyConfiguration(new StaffConfiguration());
            builder.ApplyConfiguration(new MachineConfiguration());
        }

        public DbSet<StaffEntity> StaffEntities { get; set; } = null!;
        public DbSet<DeviceDriverEntity> DeviceDriverEntities { get; set; } = null!;
        public DbSet<FarmEntity> FarmEntities { get; set; } = null!;
        public DbSet<ImageEntity> ImageEntities { get; set; } = null!;
        public DbSet<InstrumentationEntity> InstrumentationEntities { get; set; } = null!;
        public DbSet<MachineEntity> MachineEntities { get; set; } = null!;
        public DbSet<MachineWarranlyDateEntity> MachineWarranlyDateEntities { get; set; } = null!;
        public DbSet<TypeTreeEntity> TypeTreeEntities { get; set; } = null!;
        public DbSet<ZoneDeviceDriverEntity> ZoneDeviceDrivers { get; set; } = null!;
        public DbSet<ZoneEntity> ZoneEntityEntities { get; set; } = null!;
    }
}