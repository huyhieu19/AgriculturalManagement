﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgriculturalManagement.Migrations
{
    [DbContext(typeof(FactDbContext))]
    partial class FactDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.CommonType.TypeTreeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeTree", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Loại cây ăn quả có thịnh hành tại Việt Nam.",
                            NameType = "Xoài"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Loại cây ăn quả thường thấy trong vườn nhà dân.",
                            NameType = "Chuối"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Cây sầu riêng thường được trồng ở miền Nam Việt Nam.",
                            NameType = "Sầu riêng"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Loại cây ăn quả có hạt lớn và ngon.",
                            NameType = "Mít"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Vải là loại cây ăn quả có quả nhỏ màu đỏ tươi.",
                            NameType = "Vải"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Nhãn là cây ăn quả thường thấy tại Việt Nam.",
                            NameType = "Nhãn"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Loại cây ăn quả có vỏ màu đỏ và hạt trắng.",
                            NameType = "Chôm chôm"
                        },
                        new
                        {
                            Id = 8,
                            Description = "Bưởi là loại cây ăn quả có vị ngọt và hấp dẫn.",
                            NameType = "Bưởi"
                        },
                        new
                        {
                            Id = 9,
                            Description = "Cam là loại cây ăn quả chứa nhiều vitamin C.",
                            NameType = "Cam"
                        },
                        new
                        {
                            Id = 10,
                            Description = "Nho là loại cây ăn quả có nhiều loại khác nhau.",
                            NameType = "Nho"
                        },
                        new
                        {
                            Id = 11,
                            Description = "Đu đủ là cây ăn quả phổ biến ở Việt Nam.",
                            NameType = "Đu đủ"
                        },
                        new
                        {
                            Id = 12,
                            Description = "Khế là cây ăn quả có hương vị chua ngọt đặc trưng.",
                            NameType = "Khế"
                        },
                        new
                        {
                            Id = 13,
                            Description = "Loại cây ăn quả có hình dáng độc đáo.",
                            NameType = "Thanh long"
                        },
                        new
                        {
                            Id = 14,
                            Description = "Dứa là cây ăn quả thường thấy trong vườn nhà dân.",
                            NameType = "Dứa"
                        },
                        new
                        {
                            Id = 15,
                            Description = "Cây cau thường trồng ở vùng nhiệt đới Việt Nam.",
                            NameType = "Cây cau"
                        },
                        new
                        {
                            Id = 16,
                            Description = "Chanh là cây ăn quả có vị chua và mùi thơm.",
                            NameType = "Chanh"
                        },
                        new
                        {
                            Id = 17,
                            Description = "Me là cây ăn quả có vị chua ngọt đặc trưng.",
                            NameType = "Me"
                        },
                        new
                        {
                            Id = 18,
                            Description = "Sapoche là loại cây ăn quả có vị ngọt và mùi thơm đặc trưng.",
                            NameType = "Sapoche"
                        },
                        new
                        {
                            Id = 19,
                            Description = "Cherry là loại cây ăn quả có hạt nhỏ màu đỏ.",
                            NameType = "Cherry"
                        },
                        new
                        {
                            Id = 20,
                            Description = "Lựu là cây ăn quả có hình dáng đặc trưng.",
                            NameType = "Lựu"
                        });
                });

            modelBuilder.Entity("Entities.DeviceInstrumentThresholdEntity", b =>
                {
                    b.Property<Guid>("DeviceDriverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InstrumentationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DeviceEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("IsDelete")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool?>("OnInUpperThreshold")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<double?>("ThresholdValueOff")
                        .HasColumnType("float");

                    b.Property<double?>("ThresholdValueOn")
                        .HasColumnType("float");

                    b.HasKey("DeviceDriverId", "InstrumentationId");

                    b.HasIndex("DeviceEntityId");

                    b.HasIndex("InstrumentationId");

                    b.ToTable("DeviceInstrumentThreshold", (string)null);
                });

            modelBuilder.Entity("Entities.Farm.FarmEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Area")
                        .HasColumnType("float");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Farms", (string)null);
                });

            modelBuilder.Entity("Entities.Image.ImageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DeviceDriverId")
                        .HasColumnType("int");

                    b.Property<int?>("FarmId")
                        .HasColumnType("int");

                    b.Property<int?>("InstrumentationId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ZoneHarvestId")
                        .HasColumnType("int");

                    b.Property<int?>("ZoneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FarmId");

                    b.HasIndex("ZoneHarvestId");

                    b.HasIndex("ZoneId");

                    b.ToTable("Image", (string)null);
                });

            modelBuilder.Entity("Entities.JobInZoneEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameJob")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReminderDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ZoneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ZoneId");

                    b.ToTable("JobInZoneEntity");
                });

            modelBuilder.Entity("Entities.Module.DeviceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DeviceType")
                        .HasColumnType("int");

                    b.Property<string>("Gpio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAction")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAuto")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("ModuleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRef")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ResponseType")
                        .HasColumnType("int");

                    b.Property<Guid>("Topic")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ZoneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("ZoneId");

                    b.ToTable("Device", (string)null);
                });

            modelBuilder.Entity("Entities.Module.ModuleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("ClientId");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModuleType")
                        .HasColumnType("int");

                    b.Property<int>("MqttPort")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1883);

                    b.Property<string>("MqttServer")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("broker.emqx.io");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("public");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserName")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("emqx");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Module", (string)null);
                });

            modelBuilder.Entity("Entities.TimerDeviceDriverEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DeviceDriverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsAffected")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemove")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsSuccess")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("OpenTimer")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ShutDownTimer")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DeviceDriverId");

                    b.ToTable("TimerDeviceDriver", (string)null);
                });

            modelBuilder.Entity("Entities.UserEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Entities.ZoneEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double?>("Area")
                        .HasColumnType("float");

                    b.Property<DateTime?>("DateCreateFarm")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FarmId")
                        .HasColumnType("int");

                    b.Property<string>("Function")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TimeToStartPlanting")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TypeTreeId")
                        .HasColumnType("int");

                    b.Property<string>("ZoneName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FarmId");

                    b.HasIndex("TypeTreeId");

                    b.ToTable("Zone", (string)null);
                });

            modelBuilder.Entity("Entities.ZoneHarvestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("HarvertTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ZoneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ZoneId");

                    b.ToTable("ZoneHarvestEntity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "27cff7a9-5dd2-4300-9185-d7be99c4da16",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "efd37bbc-5fa6-45ba-a749-bf93cdadbf60",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("Entities.DeviceInstrumentThresholdEntity", b =>
                {
                    b.HasOne("Entities.Module.DeviceEntity", "DeviceDriver")
                        .WithMany()
                        .HasForeignKey("DeviceDriverId")
                        .IsRequired();

                    b.HasOne("Entities.Module.DeviceEntity", null)
                        .WithMany("DeviceInstrumentOnOffs")
                        .HasForeignKey("DeviceEntityId");

                    b.HasOne("Entities.Module.DeviceEntity", "DeviceInstrumentation")
                        .WithMany()
                        .HasForeignKey("InstrumentationId")
                        .IsRequired();

                    b.Navigation("DeviceDriver");

                    b.Navigation("DeviceInstrumentation");
                });

            modelBuilder.Entity("Entities.Farm.FarmEntity", b =>
                {
                    b.HasOne("Entities.UserEntity", "User")
                        .WithMany("Farms")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Image.ImageEntity", b =>
                {
                    b.HasOne("Entities.Farm.FarmEntity", "Farm")
                        .WithMany()
                        .HasForeignKey("FarmId");

                    b.HasOne("Entities.ZoneHarvestEntity", "ZoneHarvest")
                        .WithMany()
                        .HasForeignKey("ZoneHarvestId");

                    b.HasOne("Entities.ZoneEntity", "Zone")
                        .WithMany()
                        .HasForeignKey("ZoneId");

                    b.Navigation("Farm");

                    b.Navigation("Zone");

                    b.Navigation("ZoneHarvest");
                });

            modelBuilder.Entity("Entities.JobInZoneEntity", b =>
                {
                    b.HasOne("Entities.ZoneEntity", "Zone")
                        .WithMany("JobInZones")
                        .HasForeignKey("ZoneId");

                    b.Navigation("Zone");
                });

            modelBuilder.Entity("Entities.Module.DeviceEntity", b =>
                {
                    b.HasOne("Entities.Module.ModuleEntity", "Module")
                        .WithMany("Devices")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.ZoneEntity", "Zone")
                        .WithMany("Devices")
                        .HasForeignKey("ZoneId");

                    b.Navigation("Module");

                    b.Navigation("Zone");
                });

            modelBuilder.Entity("Entities.Module.ModuleEntity", b =>
                {
                    b.HasOne("Entities.UserEntity", "User")
                        .WithMany("Esps")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.TimerDeviceDriverEntity", b =>
                {
                    b.HasOne("Entities.Module.DeviceEntity", "DeviceDriver")
                        .WithMany("TimerDevices")
                        .HasForeignKey("DeviceDriverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("DeviceDriver");
                });

            modelBuilder.Entity("Entities.ZoneEntity", b =>
                {
                    b.HasOne("Entities.Farm.FarmEntity", "Farm")
                        .WithMany("Zones")
                        .HasForeignKey("FarmId");

                    b.HasOne("Entities.CommonType.TypeTreeEntity", "TypeTree")
                        .WithMany("Zones")
                        .HasForeignKey("TypeTreeId");

                    b.Navigation("Farm");

                    b.Navigation("TypeTree");
                });

            modelBuilder.Entity("Entities.ZoneHarvestEntity", b =>
                {
                    b.HasOne("Entities.ZoneEntity", "Zone")
                        .WithMany("Harvests")
                        .HasForeignKey("ZoneId");

                    b.Navigation("Zone");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.CommonType.TypeTreeEntity", b =>
                {
                    b.Navigation("Zones");
                });

            modelBuilder.Entity("Entities.Farm.FarmEntity", b =>
                {
                    b.Navigation("Zones");
                });

            modelBuilder.Entity("Entities.Module.DeviceEntity", b =>
                {
                    b.Navigation("DeviceInstrumentOnOffs");

                    b.Navigation("TimerDevices");
                });

            modelBuilder.Entity("Entities.Module.ModuleEntity", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("Entities.UserEntity", b =>
                {
                    b.Navigation("Esps");

                    b.Navigation("Farms");
                });

            modelBuilder.Entity("Entities.ZoneEntity", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("Harvests");

                    b.Navigation("JobInZones");
                });
#pragma warning restore 612, 618
        }
    }
}
