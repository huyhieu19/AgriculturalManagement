using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class configColumn202311101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceDriver_Esp_EspId",
                table: "DeviceDriver");

            migrationBuilder.DropForeignKey(
                name: "FK_Instrumentation_Esp_Esp8266Id",
                table: "Instrumentation");

            migrationBuilder.DropIndex(
                name: "IX_Instrumentation_Esp8266Id",
                table: "Instrumentation");

            migrationBuilder.DropIndex(
                name: "IX_DeviceDriver_EspId",
                table: "DeviceDriver");

            migrationBuilder.DropColumn(
                name: "Esp8266Id",
                table: "Instrumentation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Esp8266Id",
                table: "Instrumentation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instrumentation_Esp8266Id",
                table: "Instrumentation",
                column: "Esp8266Id");

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
                name: "FK_Instrumentation_Esp_Esp8266Id",
                table: "Instrumentation",
                column: "Esp8266Id",
                principalTable: "Esp",
                principalColumn: "Id");
        }
    }
}
