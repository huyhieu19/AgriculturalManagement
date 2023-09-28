using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class Update202309282 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8e533c0b-d0fa-4a96-a274-02298b418217");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ae85d355-a9e2-4717-9e72-ebd644f7158d");

            migrationBuilder.DropColumn(
                name: "IsAuto",
                table: "DeviceDriver");

            migrationBuilder.DropColumn(
                name: "IsDaily",
                table: "DeviceDriver");

            migrationBuilder.DropColumn(
                name: "OpenTimer",
                table: "DeviceDriver");

            migrationBuilder.DropColumn(
                name: "ShutDownTime",
                table: "DeviceDriver");

            migrationBuilder.CreateTable(
                name: "TimerDeviceDriver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDaily = table.Column<bool>(type: "bit", nullable: true),
                    IsAuto = table.Column<bool>(type: "bit", nullable: true),
                    ShutDownTimer = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OpenTimer = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeviceDriverId = table.Column<int>(type: "int", nullable: true),
                    IsRemove = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ac24a0c9-848a-401f-ad70-d8804c2158f9", null, "Administrator", "ADMINISTRATOR" },
                    { "e21d687f-cfe5-473a-a716-84e9cbc59de0", null, "Manager", "MANAGER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimerDeviceDriver_DeviceDriverId",
                table: "TimerDeviceDriver",
                column: "DeviceDriverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimerDeviceDriver");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ac24a0c9-848a-401f-ad70-d8804c2158f9");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e21d687f-cfe5-473a-a716-84e9cbc59de0");

            migrationBuilder.AddColumn<bool>(
                name: "IsAuto",
                table: "DeviceDriver",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDaily",
                table: "DeviceDriver",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpenTimer",
                table: "DeviceDriver",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShutDownTime",
                table: "DeviceDriver",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8e533c0b-d0fa-4a96-a274-02298b418217", null, "Administrator", "ADMINISTRATOR" },
                    { "ae85d355-a9e2-4717-9e72-ebd644f7158d", null, "Manager", "MANAGER" }
                });
        }
    }
}
