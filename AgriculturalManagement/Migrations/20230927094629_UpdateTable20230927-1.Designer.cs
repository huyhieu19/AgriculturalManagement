﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgriculturalManagement.Migrations
{
    [DbContext(typeof(FactDbContext))]
    [Migration("20230927094629_UpdateTable20230927-1")]
    partial class UpdateTable202309271
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.DeviceDriverEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateStartedUsing")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeviceDriverTypeId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsAction")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsAuto")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDaily")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsProblem")
                        .HasColumnType("bit");

                    b.Property<int?>("OpenTimer")
                        .HasColumnType("int");

                    b.Property<int?>("ShutDownTime")
                        .HasColumnType("int");

                    b.Property<int?>("ZoneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeviceDriverTypeId");

                    b.HasIndex("ZoneId");

                    b.ToTable("DeviceDriver", (string)null);
                });

            modelBuilder.Entity("Entities.DeviceDriverTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DeviceDriverType", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Máy bơm"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Quạt gió"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Rèm cửa"
                        });
                });

            modelBuilder.Entity("Entities.FarmEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Farm", (string)null);
                });

            modelBuilder.Entity("Entities.ImageEntity", b =>
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

                    b.HasIndex("DeviceDriverId");

                    b.HasIndex("FarmId");

                    b.HasIndex("InstrumentationId");

                    b.HasIndex("ZoneHarvestId");

                    b.HasIndex("ZoneId");

                    b.ToTable("Image", (string)null);
                });

            modelBuilder.Entity("Entities.InstrumentationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateStartedUsing")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InstrumentationTypeId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ZoneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InstrumentationTypeId");

                    b.HasIndex("ZoneId");

                    b.ToTable("Instrumentation", (string)null);
                });

            modelBuilder.Entity("Entities.InstrumentationTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InstrumentationType", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Cảm biến đo nhiệt độ",
                            Unit = "*C"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Cảm biến đo độ ẩm không khí",
                            Unit = "%"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Cảm biến nước mưa"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Cảm biên gió",
                            Unit = "Km/h"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Độ ẩm đất",
                            Unit = "%"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Cảm biên đo độ PH của đất",
                            Unit = "PH"
                        });
                });

            modelBuilder.Entity("Entities.JobInZoneEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

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

                    b.ToTable("JobInZone", (string)null);
                });

            modelBuilder.Entity("Entities.MachineEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateStartedUsing")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Machine", (string)null);
                });

            modelBuilder.Entity("Entities.MachineWarranlyDateEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DeviceDriversId")
                        .HasColumnType("int");

                    b.Property<int>("InstrumentationId")
                        .HasColumnType("int");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("WarrantyDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DeviceDriversId");

                    b.HasIndex("InstrumentationId");

                    b.HasIndex("MachineId");

                    b.ToTable("MachineWarranlyDate", (string)null);
                });

            modelBuilder.Entity("Entities.TypeTreeEntity", b =>
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

            modelBuilder.Entity("Entities.UserEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AvatarUrl")
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

                    b.ToTable("ZoneHarvest", (string)null);
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
                            Id = "ae85d355-a9e2-4717-9e72-ebd644f7158d",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "8e533c0b-d0fa-4a96-a274-02298b418217",
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

            modelBuilder.Entity("Entities.DeviceDriverEntity", b =>
                {
                    b.HasOne("Entities.DeviceDriverTypeEntity", "DeviceDriverType")
                        .WithMany("DeviceDrivers")
                        .HasForeignKey("DeviceDriverTypeId");

                    b.HasOne("Entities.ZoneEntity", "Zone")
                        .WithMany("ZoneDeviceDrivers")
                        .HasForeignKey("ZoneId");

                    b.Navigation("DeviceDriverType");

                    b.Navigation("Zone");
                });

            modelBuilder.Entity("Entities.FarmEntity", b =>
                {
                    b.HasOne("Entities.UserEntity", "User")
                        .WithMany("Farms")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.ImageEntity", b =>
                {
                    b.HasOne("Entities.DeviceDriverEntity", "DeviceDriver")
                        .WithMany("Images")
                        .HasForeignKey("DeviceDriverId");

                    b.HasOne("Entities.FarmEntity", "Farm")
                        .WithMany("Images")
                        .HasForeignKey("FarmId");

                    b.HasOne("Entities.InstrumentationEntity", "Instrumentation")
                        .WithMany("Images")
                        .HasForeignKey("InstrumentationId");

                    b.HasOne("Entities.ZoneHarvestEntity", "ZoneHarvest")
                        .WithMany("Images")
                        .HasForeignKey("ZoneHarvestId");

                    b.HasOne("Entities.ZoneEntity", "Zone")
                        .WithMany("Images")
                        .HasForeignKey("ZoneId");

                    b.Navigation("DeviceDriver");

                    b.Navigation("Farm");

                    b.Navigation("Instrumentation");

                    b.Navigation("Zone");

                    b.Navigation("ZoneHarvest");
                });

            modelBuilder.Entity("Entities.InstrumentationEntity", b =>
                {
                    b.HasOne("Entities.InstrumentationTypeEntity", "InstrumentationType")
                        .WithMany("Instrumentations")
                        .HasForeignKey("InstrumentationTypeId");

                    b.HasOne("Entities.ZoneEntity", "Zone")
                        .WithMany("Instrumentations")
                        .HasForeignKey("ZoneId");

                    b.Navigation("InstrumentationType");

                    b.Navigation("Zone");
                });

            modelBuilder.Entity("Entities.JobInZoneEntity", b =>
                {
                    b.HasOne("Entities.ZoneEntity", "Zone")
                        .WithMany("JobInZones")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Zone");
                });

            modelBuilder.Entity("Entities.MachineWarranlyDateEntity", b =>
                {
                    b.HasOne("Entities.DeviceDriverEntity", "DeviceDriver")
                        .WithMany("MachineWarranlyDates")
                        .HasForeignKey("DeviceDriversId")
                        .IsRequired();

                    b.HasOne("Entities.InstrumentationEntity", "Instrumentation")
                        .WithMany("MachineWarranlyDates")
                        .HasForeignKey("InstrumentationId")
                        .IsRequired();

                    b.HasOne("Entities.MachineEntity", "Machine")
                        .WithMany("MachineWarranlyDates")
                        .HasForeignKey("MachineId")
                        .IsRequired();

                    b.Navigation("DeviceDriver");

                    b.Navigation("Instrumentation");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Entities.ZoneEntity", b =>
                {
                    b.HasOne("Entities.FarmEntity", "Farm")
                        .WithMany("Zones")
                        .HasForeignKey("FarmId");

                    b.HasOne("Entities.TypeTreeEntity", "TypeTree")
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

            modelBuilder.Entity("Entities.DeviceDriverEntity", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("MachineWarranlyDates");
                });

            modelBuilder.Entity("Entities.DeviceDriverTypeEntity", b =>
                {
                    b.Navigation("DeviceDrivers");
                });

            modelBuilder.Entity("Entities.FarmEntity", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Zones");
                });

            modelBuilder.Entity("Entities.InstrumentationEntity", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("MachineWarranlyDates");
                });

            modelBuilder.Entity("Entities.InstrumentationTypeEntity", b =>
                {
                    b.Navigation("Instrumentations");
                });

            modelBuilder.Entity("Entities.MachineEntity", b =>
                {
                    b.Navigation("MachineWarranlyDates");
                });

            modelBuilder.Entity("Entities.TypeTreeEntity", b =>
                {
                    b.Navigation("Zones");
                });

            modelBuilder.Entity("Entities.UserEntity", b =>
                {
                    b.Navigation("Farms");
                });

            modelBuilder.Entity("Entities.ZoneEntity", b =>
                {
                    b.Navigation("Harvests");

                    b.Navigation("Images");

                    b.Navigation("Instrumentations");

                    b.Navigation("JobInZones");

                    b.Navigation("ZoneDeviceDrivers");
                });

            modelBuilder.Entity("Entities.ZoneHarvestEntity", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
