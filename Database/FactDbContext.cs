using Database.ModelCreateConfiguration;
using Database.ModelCreateConfiguration.Role;
using Entities;
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

            //Farm configuration
            builder.ApplyConfiguration(new FarmConfiguration());

            builder.ApplyConfiguration(new InstrumentationConfiguration());
            builder.ApplyConfiguration(new InstrumentationTypeConfiguration());


            builder.ApplyConfiguration(new MachineWarranlyDateConfiguration());

            // Zone Configuration
            builder.ApplyConfiguration(new ZoneConfiguration());
            builder.ApplyConfiguration(new ZoneHarvestConfiguration());
            builder.ApplyConfiguration(new JobInZoneConfiguration());

            // Device Driver Configuration
            builder.ApplyConfiguration(new DeviceDriverConfiguration());
            builder.ApplyConfiguration(new DeviceDriverTypeConfiguration());
            builder.ApplyConfiguration(new TimerDeviceDriverConfiguration());

            builder.ApplyConfiguration(new TypeTreeConfiguration());
            builder.ApplyConfiguration(new MachineConfiguration());


        }
        // DeviceDriver Dataset
        public DbSet<DeviceDriverEntity> DeviceDriverEntities { get; set; } = null!;
        public DbSet<TimerDeviceDriverEntity> TimerDeviceDriverEntities { get; set; } = null!;
        public DbSet<DeviceDriverTypeEntity> DeviceDriversTypeEntities { get; set; } = null!;

        // Farm
        public DbSet<FarmEntity> FarmEntities { get; set; } = null!;

        // Image
        public DbSet<ImageEntity> ImageEntities { get; set; } = null!;

        // Instrumentation
        public DbSet<InstrumentationEntity> InstrumentationEntities { get; set; } = null!;
        // Machine
        public DbSet<MachineEntity> MachineEntities { get; set; } = null!;
        public DbSet<MachineWarranlyDateEntity> MachineWarranlyDateEntities { get; set; } = null!;

        // Type
        public DbSet<TypeTreeEntity> TypeTreeEntities { get; set; } = null!;
        public DbSet<InstrumentationTypeEntity> InstrumentationTypeEntities { get; set; } = null!;

        // Zone 
        public DbSet<ZoneEntity> ZoneEntityEntities { get; set; } = null!;
        public DbSet<ZoneHarvestEntity> ZoneHarvestEntities { get; set; } = null!;
        public DbSet<JobInZoneEntity> JobInZoneEntities { get; set; } = null!;

    }
}