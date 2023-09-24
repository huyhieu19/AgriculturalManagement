using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class Updatedatabase20230924 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DeviceDriver");

            migrationBuilder.AddColumn<int>(
                name: "DeviceDriverId",
                table: "Image",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstrumentationId",
                table: "Image",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "Image",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfPurChanse",
                table: "DeviceDriver",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_DeviceDriverId",
                table: "Image",
                column: "DeviceDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_InstrumentationId",
                table: "Image",
                column: "InstrumentationId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_MachineId",
                table: "Image",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_DeviceDriver_DeviceDriverId",
                table: "Image",
                column: "DeviceDriverId",
                principalTable: "DeviceDriver",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Instrumentation_InstrumentationId",
                table: "Image",
                column: "InstrumentationId",
                principalTable: "Instrumentation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Machine_MachineId",
                table: "Image",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_DeviceDriver_DeviceDriverId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Instrumentation_InstrumentationId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Machine_MachineId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_DeviceDriverId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_InstrumentationId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_MachineId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "DeviceDriverId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "InstrumentationId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "DateOfPurChanse",
                table: "DeviceDriver");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DeviceDriver",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
