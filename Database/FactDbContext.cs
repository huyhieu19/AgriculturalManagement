using Database.ModelCreateConfiguration;
using Database.ModelCreateConfiguration.Role;
using Entities;
using Entities.Farm;
using Entities.Image;
using Entities.Module;
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
            // User
            // Identity Role
            builder.ApplyConfiguration(new RoleConfiguration());

            // Image configuration
            builder.ApplyConfiguration(new ImageConfiguration());

            //Farms configuration
            builder.ApplyConfiguration(new FarmConfiguration());

            // Zone Configuration
            builder.ApplyConfiguration(new ZoneConfiguration());

            // Device Driver Configuration
            builder.ApplyConfiguration(new TimerDeviceDriverConfiguration());

            // InstrumentSetThreshold
            builder.ApplyConfiguration(new DeviceInstrumentThresholdConfiguration());

            //Module
            builder.ApplyConfiguration(new ModuleConfigurarion());
            builder.ApplyConfiguration(new DeviceConfiguration());
        }
        // DeviceDriver DataSet
        public DbSet<TimerDeviceEntity> TimerDeviceDriverEntities { get; set; } = null!;

        // Farms
        public DbSet<FarmEntity> FarmEntities { get; set; } = null!;

        // Image
        public DbSet<ImageEntity> ImageEntities { get; set; } = null!;

        // Zone 
        public DbSet<ZoneEntity> ZoneEntityEntities { get; set; } = null!;

        // InstrumentSetThreshold
        public DbSet<ThresholdDeviceEntity> DeviceInstrumentThresholdEntities { get; set; } = null!;

        // ESP
        public DbSet<ModuleEntity> ModuleEntities { get; set; } = null!;
        public DbSet<DeviceEntity> DeviceEntities { get; set; } = null!;
    }
}