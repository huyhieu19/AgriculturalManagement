using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable20231023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "12c6f030-ade2-47c0-9ea5-c7cb07ef26af");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "fb931a45-9cfd-4f3c-bb98-6df852751f83");

            migrationBuilder.AddColumn<Guid>(
                name: "EspId",
                table: "Instrumentation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gpio",
                table: "Instrumentation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EspId",
                table: "DeviceDriver",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gpio",
                table: "DeviceDriver",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DeviceDriver",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Esp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Esp", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9432e467-aa3a-4c0e-bfa3-e1d94bedd37a", null, "Manager", "MANAGER" },
                    { "e17f0f08-dd77-4e59-9684-e0871fa0f61c", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instrumentation_EspId",
                table: "Instrumentation",
                column: "EspId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDriver_EspId",
                table: "DeviceDriver",
                column: "EspId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceDriver_Esp_EspId",
                table: "DeviceDriver",
                column: "EspId",
                principalTable: "Esp",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instrumentation_Esp_EspId",
                table: "Instrumentation",
                column: "EspId",
                principalTable: "Esp",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceDriver_Esp_EspId",
                table: "DeviceDriver");

            migrationBuilder.DropForeignKey(
                name: "FK_Instrumentation_Esp_EspId",
                table: "Instrumentation");

            migrationBuilder.DropTable(
                name: "Esp");

            migrationBuilder.DropIndex(
                name: "IX_Instrumentation_EspId",
                table: "Instrumentation");

            migrationBuilder.DropIndex(
                name: "IX_DeviceDriver_EspId",
                table: "DeviceDriver");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9432e467-aa3a-4c0e-bfa3-e1d94bedd37a");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e17f0f08-dd77-4e59-9684-e0871fa0f61c");

            migrationBuilder.DropColumn(
                name: "EspId",
                table: "Instrumentation");

            migrationBuilder.DropColumn(
                name: "Gpio",
                table: "Instrumentation");

            migrationBuilder.DropColumn(
                name: "EspId",
                table: "DeviceDriver");

            migrationBuilder.DropColumn(
                name: "Gpio",
                table: "DeviceDriver");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DeviceDriver");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12c6f030-ade2-47c0-9ea5-c7cb07ef26af", null, "Manager", "MANAGER" },
                    { "fb931a45-9cfd-4f3c-bb98-6df852751f83", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
