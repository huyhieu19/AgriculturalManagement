using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class update20231005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5047a40a-762e-40f5-9a6a-a394410c5f9b");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6d8faace-8aa7-43fe-9e64-30305c308175");

            migrationBuilder.DropColumn(
                name: "ThresholdValueOff",
                table: "TimerDeviceDriver");

            migrationBuilder.DropColumn(
                name: "ThresholdValueOn",
                table: "TimerDeviceDriver");

            migrationBuilder.CreateTable(
                name: "DeviceInstrumentOnOffEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceDriverId = table.Column<int>(type: "int", nullable: true),
                    InstrumentationId = table.Column<int>(type: "int", nullable: true),
                    ThresholdValueOn = table.Column<double>(type: "float", nullable: true),
                    ThresholdValueOff = table.Column<double>(type: "float", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceInstrumentOnOffEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceInstrumentOnOffEntity_DeviceDriver_DeviceDriverId",
                        column: x => x.DeviceDriverId,
                        principalTable: "DeviceDriver",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeviceInstrumentOnOffEntity_Instrumentation_InstrumentationId",
                        column: x => x.InstrumentationId,
                        principalTable: "Instrumentation",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15fb6761-a08d-44e8-b095-1dec117c7d54", null, "Manager", "MANAGER" },
                    { "628f0ce7-ab09-44ae-bbeb-8ae8a03174ac", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceInstrumentOnOffEntity_DeviceDriverId",
                table: "DeviceInstrumentOnOffEntity",
                column: "DeviceDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceInstrumentOnOffEntity_InstrumentationId",
                table: "DeviceInstrumentOnOffEntity",
                column: "InstrumentationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceInstrumentOnOffEntity");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "15fb6761-a08d-44e8-b095-1dec117c7d54");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "628f0ce7-ab09-44ae-bbeb-8ae8a03174ac");

            migrationBuilder.AddColumn<double>(
                name: "ThresholdValueOff",
                table: "TimerDeviceDriver",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ThresholdValueOn",
                table: "TimerDeviceDriver",
                type: "float",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5047a40a-762e-40f5-9a6a-a394410c5f9b", null, "Manager", "MANAGER" },
                    { "6d8faace-8aa7-43fe-9e64-30305c308175", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
