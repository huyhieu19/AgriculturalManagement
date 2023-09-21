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

            // Image configuration
            builder.ApplyConfiguration(new ImageConfiguration());

            //Farm configuration
            builder.Entity<FarmEntity>().HasOne(p => p.User).WithMany(p => p.Farms).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.SetNull);
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