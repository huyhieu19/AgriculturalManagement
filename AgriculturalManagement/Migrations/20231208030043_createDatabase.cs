using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class createDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Farms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<double>(type: "float", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Farms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleType = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NameRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MqttServer = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "broker.emqx.io"),
                    MqttPort = table.Column<int>(type: "int", nullable: false, defaultValue: 1883),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "ClientId"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "emqx"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "public")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Module_Users_UserId",
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FarmId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zone_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZoneId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAction = table.Column<bool>(type: "bit", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsAuto = table.Column<bool>(type: "bit", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Device_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Device_Zone_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zone",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobInZoneEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameJob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReminderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZoneId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobInZoneEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobInZoneEntity_Zone_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zone",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ZoneHarvestEntity",
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
                    table.PrimaryKey("PK_ZoneHarvestEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZoneHarvestEntity_Zone_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zone",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeviceInstrumentThreshold",
                columns: table => new
                {
                    DeviceDriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstrumentationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    OnInUpperThreshold = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    ThresholdValueOn = table.Column<double>(type: "float", nullable: true),
                    ThresholdValueOff = table.Column<double>(type: "float", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeviceEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceInstrumentThreshold", x => new { x.DeviceDriverId, x.InstrumentationId });
                    table.ForeignKey(
                        name: "FK_DeviceInstrumentThreshold_Device_DeviceDriverId",
                        column: x => x.DeviceDriverId,
                        principalTable: "Device",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeviceInstrumentThreshold_Device_DeviceEntityId",
                        column: x => x.DeviceEntityId,
                        principalTable: "Device",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeviceInstrumentThreshold_Device_InstrumentationId",
                        column: x => x.InstrumentationId,
                        principalTable: "Device",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimerDeviceDriver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShutDownTimer = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OpenTimer = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSuccessON = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsSuccessOFF = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsRemove = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimerDeviceDriver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimerDeviceDriver_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
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
                        name: "FK_Image_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Image_ZoneHarvestEntity_ZoneHarvestId",
                        column: x => x.ZoneHarvestId,
                        principalTable: "ZoneHarvestEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Image_Zone_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zone",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "27cff7a9-5dd2-4300-9185-d7be99c4da16", null, "Manager", "MANAGER" },
                    { "efd37bbc-5fa6-45ba-a749-bf93cdadbf60", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Device_ModuleId",
                table: "Device",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_ZoneId",
                table: "Device",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceInstrumentThreshold_DeviceEntityId",
                table: "DeviceInstrumentThreshold",
                column: "DeviceEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceInstrumentThreshold_InstrumentationId",
                table: "DeviceInstrumentThreshold",
                column: "InstrumentationId");

            migrationBuilder.CreateIndex(
                name: "IX_Farms_UserId",
                table: "Farms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_FarmId",
                table: "Image",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ZoneHarvestId",
                table: "Image",
                column: "ZoneHarvestId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ZoneId",
                table: "Image",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_JobInZoneEntity_ZoneId",
                table: "JobInZoneEntity",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_UserId",
                table: "Module",
                column: "UserId");

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
                name: "IX_TimerDeviceDriver_DeviceId",
                table: "TimerDeviceDriver",
                column: "DeviceId");

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
                name: "IX_ZoneHarvestEntity_ZoneId",
                table: "ZoneHarvestEntity",
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
                name: "JobInZoneEntity");

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
                name: "ZoneHarvestEntity");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "Zone");

            migrationBuilder.DropTable(
                name: "Farms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
