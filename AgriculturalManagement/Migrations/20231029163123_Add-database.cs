﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class Adddatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceDriverType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDriverType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Esp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MqttServer = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "ff45c897-e383-44a7-ad38-10d27785f315"),
                    MqttPort = table.Column<int>(type: "int", nullable: false, defaultValue: 1883),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "ClientId"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "emqx"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "public"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Esp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentationType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Machine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateStartedUsing = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeTree",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTree", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Farm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<double>(type: "float", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Farm_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZoneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<double>(type: "float", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeToStartPlanting = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreateFarm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FarmId = table.Column<int>(type: "int", nullable: true),
                    TypeTreeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zone_Farm_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Zone_TypeTree_TypeTreeId",
                        column: x => x.TypeTreeId,
                        principalTable: "TypeTree",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeviceDriver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateStartedUsing = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAction = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsAuto = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeviceDriverTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ZoneId = table.Column<int>(type: "int", nullable: true),
                    EspId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Gpio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "D")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDriver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceDriver_DeviceDriverType_DeviceDriverTypeId",
                        column: x => x.DeviceDriverTypeId,
                        principalTable: "DeviceDriverType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeviceDriver_Esp_EspId",
                        column: x => x.EspId,
                        principalTable: "Esp",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeviceDriver_Zone_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zone",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Instrumentation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    DateStartedUsing = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ZoneId = table.Column<int>(type: "int", nullable: true),
                    InstrumentationTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EspId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Gpio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "I")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrumentation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instrumentation_Esp_EspId",
                        column: x => x.EspId,
                        principalTable: "Esp",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Instrumentation_InstrumentationType_InstrumentationTypeId",
                        column: x => x.InstrumentationTypeId,
                        principalTable: "InstrumentationType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Instrumentation_Zone_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zone",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobInZone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NameJob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReminderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZoneId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobInZone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobInZone_Zone_Id",
                        column: x => x.Id,
                        principalTable: "Zone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZoneHarvest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HarvertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZoneId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneHarvest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZoneHarvest_Zone_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zone",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimerDeviceDriver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDaily = table.Column<bool>(type: "bit", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShutDownTimer = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OpenTimer = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAffected = table.Column<bool>(type: "bit", nullable: true),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsRemove = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeviceDriverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimerDeviceDriver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimerDeviceDriver_DeviceDriver_DeviceDriverId",
                        column: x => x.DeviceDriverId,
                        principalTable: "DeviceDriver",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeviceInstrumentThreshold",
                columns: table => new
                {
                    DeviceDriverId = table.Column<int>(type: "int", nullable: false),
                    InstrumentationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    OnInUpperThreshold = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    ThresholdValueOn = table.Column<double>(type: "float", nullable: true),
                    ThresholdValueOff = table.Column<double>(type: "float", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceInstrumentThreshold", x => new { x.DeviceDriverId, x.InstrumentationId });
                    table.ForeignKey(
                        name: "FK_DeviceInstrumentThreshold_DeviceDriver_DeviceDriverId",
                        column: x => x.DeviceDriverId,
                        principalTable: "DeviceDriver",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeviceInstrumentThreshold_Instrumentation_InstrumentationId",
                        column: x => x.InstrumentationId,
                        principalTable: "Instrumentation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MachineWarranlyDate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarrantyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    InstrumentationId = table.Column<int>(type: "int", nullable: false),
                    DeviceDriversId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineWarranlyDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineWarranlyDate_DeviceDriver_DeviceDriversId",
                        column: x => x.DeviceDriversId,
                        principalTable: "DeviceDriver",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineWarranlyDate_Instrumentation_InstrumentationId",
                        column: x => x.InstrumentationId,
                        principalTable: "Instrumentation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineWarranlyDate_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FarmId = table.Column<int>(type: "int", nullable: true),
                    ZoneId = table.Column<int>(type: "int", nullable: true),
                    ZoneHarvestId = table.Column<int>(type: "int", nullable: true),
                    InstrumentationId = table.Column<int>(type: "int", nullable: true),
                    DeviceDriverId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_DeviceDriver_DeviceDriverId",
                        column: x => x.DeviceDriverId,
                        principalTable: "DeviceDriver",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Image_Farm_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Image_Instrumentation_InstrumentationId",
                        column: x => x.InstrumentationId,
                        principalTable: "Instrumentation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Image_ZoneHarvest_ZoneHarvestId",
                        column: x => x.ZoneHarvestId,
                        principalTable: "ZoneHarvest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Image_Zone_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zone",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "DeviceDriverType",
                columns: new[] { "Id", "Description", "Manufacturer", "Name" },
                values: new object[,]
                {
                    { new Guid("032d4594-88fd-43af-bb83-9ea4351ed488"), null, null, "Quạt gió" },
                    { new Guid("0f0ae0bc-0454-42db-8c21-338a69448925"), null, null, "Quạt gió" },
                    { new Guid("134d6c68-d44c-4bad-b1e5-8e30aadf2c53"), null, null, "Rèm cửa" },
                    { new Guid("448baf97-9401-4aaa-a636-9d8512d7c5a4"), null, null, "Máy bơm" },
                    { new Guid("add310fe-34e9-4b07-8d66-38a16bc2b177"), null, null, "Máy bơm" }
                });

            migrationBuilder.InsertData(
                table: "InstrumentationType",
                columns: new[] { "Id", "Description", "Manufacturer", "Name", "Unit" },
                values: new object[,]
                {
                    { new Guid("1c44a06c-e539-4ead-8c62-fc7d56ec9e34"), null, null, "Độ ẩm đất", "%" },
                    { new Guid("3057e9c9-b039-489b-be7b-581a751ca4cb"), null, null, "Cảm biên đo độ PH của đất", "PH" },
                    { new Guid("743b3068-bb94-41f4-bd10-7f4ed802a36c"), null, null, "Cảm biến đo nhiệt độ, độ ẩm không khí", "*C/%" },
                    { new Guid("97f47bef-17cc-45c3-9fbf-69ef9364e12f"), null, null, "Cảm biên gió", "Km/h" },
                    { new Guid("b6976895-e254-487e-a59c-c22621b2c54a"), null, null, "Cảm biến nước mưa", "true/false" },
                    { new Guid("d67fb159-c95d-4c67-8f2e-065f14fc5e58"), null, null, "Cảm biến đo nhiệt độ, độ ẩm không khí", "*C/%" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "27cff7a9-5dd2-4300-9185-d7be99c4da16", null, "Manager", "MANAGER" },
                    { "efd37bbc-5fa6-45ba-a749-bf93cdadbf60", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "TypeTree",
                columns: new[] { "Id", "Description", "ImageUrl", "NameType", "Note" },
                values: new object[,]
                {
                    { 1, "Loại cây ăn quả có thịnh hành tại Việt Nam.", null, "Xoài", null },
                    { 2, "Loại cây ăn quả thường thấy trong vườn nhà dân.", null, "Chuối", null },
                    { 3, "Cây sầu riêng thường được trồng ở miền Nam Việt Nam.", null, "Sầu riêng", null },
                    { 4, "Loại cây ăn quả có hạt lớn và ngon.", null, "Mít", null },
                    { 5, "Vải là loại cây ăn quả có quả nhỏ màu đỏ tươi.", null, "Vải", null },
                    { 6, "Nhãn là cây ăn quả thường thấy tại Việt Nam.", null, "Nhãn", null },
                    { 7, "Loại cây ăn quả có vỏ màu đỏ và hạt trắng.", null, "Chôm chôm", null },
                    { 8, "Bưởi là loại cây ăn quả có vị ngọt và hấp dẫn.", null, "Bưởi", null },
                    { 9, "Cam là loại cây ăn quả chứa nhiều vitamin C.", null, "Cam", null },
                    { 10, "Nho là loại cây ăn quả có nhiều loại khác nhau.", null, "Nho", null },
                    { 11, "Đu đủ là cây ăn quả phổ biến ở Việt Nam.", null, "Đu đủ", null },
                    { 12, "Khế là cây ăn quả có hương vị chua ngọt đặc trưng.", null, "Khế", null },
                    { 13, "Loại cây ăn quả có hình dáng độc đáo.", null, "Thanh long", null },
                    { 14, "Dứa là cây ăn quả thường thấy trong vườn nhà dân.", null, "Dứa", null },
                    { 15, "Cây cau thường trồng ở vùng nhiệt đới Việt Nam.", null, "Cây cau", null },
                    { 16, "Chanh là cây ăn quả có vị chua và mùi thơm.", null, "Chanh", null },
                    { 17, "Me là cây ăn quả có vị chua ngọt đặc trưng.", null, "Me", null },
                    { 18, "Sapoche là loại cây ăn quả có vị ngọt và mùi thơm đặc trưng.", null, "Sapoche", null },
                    { 19, "Cherry là loại cây ăn quả có hạt nhỏ màu đỏ.", null, "Cherry", null },
                    { 20, "Lựu là cây ăn quả có hình dáng đặc trưng.", null, "Lựu", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDriver_DeviceDriverTypeId",
                table: "DeviceDriver",
                column: "DeviceDriverTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDriver_EspId",
                table: "DeviceDriver",
                column: "EspId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDriver_ZoneId",
                table: "DeviceDriver",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceInstrumentThreshold_InstrumentationId",
                table: "DeviceInstrumentThreshold",
                column: "InstrumentationId");

            migrationBuilder.CreateIndex(
                name: "IX_Farm_UserId",
                table: "Farm",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_DeviceDriverId",
                table: "Image",
                column: "DeviceDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_FarmId",
                table: "Image",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_InstrumentationId",
                table: "Image",
                column: "InstrumentationId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ZoneHarvestId",
                table: "Image",
                column: "ZoneHarvestId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ZoneId",
                table: "Image",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Instrumentation_EspId",
                table: "Instrumentation",
                column: "EspId");

            migrationBuilder.CreateIndex(
                name: "IX_Instrumentation_InstrumentationTypeId",
                table: "Instrumentation",
                column: "InstrumentationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Instrumentation_ZoneId",
                table: "Instrumentation",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineWarranlyDate_DeviceDriversId",
                table: "MachineWarranlyDate",
                column: "DeviceDriversId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineWarranlyDate_InstrumentationId",
                table: "MachineWarranlyDate",
                column: "InstrumentationId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineWarranlyDate_MachineId",
                table: "MachineWarranlyDate",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TimerDeviceDriver_DeviceDriverId",
                table: "TimerDeviceDriver",
                column: "DeviceDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_FarmId",
                table: "Zone",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_TypeTreeId",
                table: "Zone",
                column: "TypeTreeId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneHarvest_ZoneId",
                table: "ZoneHarvest",
                column: "ZoneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceInstrumentThreshold");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "JobInZone");

            migrationBuilder.DropTable(
                name: "MachineWarranlyDate");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "TimerDeviceDriver");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "ZoneHarvest");

            migrationBuilder.DropTable(
                name: "Instrumentation");

            migrationBuilder.DropTable(
                name: "Machine");

            migrationBuilder.DropTable(
                name: "DeviceDriver");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "InstrumentationType");

            migrationBuilder.DropTable(
                name: "DeviceDriverType");

            migrationBuilder.DropTable(
                name: "Esp");

            migrationBuilder.DropTable(
                name: "Zone");

            migrationBuilder.DropTable(
                name: "Farm");

            migrationBuilder.DropTable(
                name: "TypeTree");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}