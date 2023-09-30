using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class Update202309232 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "698715da-7735-41b8-8533-4c5f9c6b8b57");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c400bf07-1713-452c-92e2-06f1456b6a64");

            migrationBuilder.DropColumn(
                name: "IsAuto",
                table: "TimerDeviceDriver");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShutDownTimer",
                table: "TimerDeviceDriver",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenTimer",
                table: "TimerDeviceDriver",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "IsRemove",
                table: "TimerDeviceDriver",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "DeviceDriverId",
                table: "TimerDeviceDriver",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "TimerDeviceDriver",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAffected",
                table: "TimerDeviceDriver",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccess",
                table: "TimerDeviceDriver",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "TimerDeviceDriver",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AlterColumn<bool>(
                name: "IsProblem",
                table: "DeviceDriver",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsAction",
                table: "DeviceDriver",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAuto",
                table: "DeviceDriver",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5047a40a-762e-40f5-9a6a-a394410c5f9b", null, "Manager", "MANAGER" },
                    { "6d8faace-8aa7-43fe-9e64-30305c308175", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "DateCreated",
                table: "TimerDeviceDriver");

            migrationBuilder.DropColumn(
                name: "IsAffected",
                table: "TimerDeviceDriver");

            migrationBuilder.DropColumn(
                name: "IsSuccess",
                table: "TimerDeviceDriver");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "TimerDeviceDriver");

            migrationBuilder.DropColumn(
                name: "ThresholdValueOff",
                table: "TimerDeviceDriver");

            migrationBuilder.DropColumn(
                name: "ThresholdValueOn",
                table: "TimerDeviceDriver");

            migrationBuilder.DropColumn(
                name: "IsAuto",
                table: "DeviceDriver");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShutDownTimer",
                table: "TimerDeviceDriver",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenTimer",
                table: "TimerDeviceDriver",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsRemove",
                table: "TimerDeviceDriver",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "DeviceDriverId",
                table: "TimerDeviceDriver",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsAuto",
                table: "TimerDeviceDriver",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsProblem",
                table: "DeviceDriver",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsAction",
                table: "DeviceDriver",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "698715da-7735-41b8-8533-4c5f9c6b8b57", null, "Manager", "MANAGER" },
                    { "c400bf07-1713-452c-92e2-06f1456b6a64", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
