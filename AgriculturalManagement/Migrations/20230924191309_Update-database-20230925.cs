using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class Updatedatabase20230925 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Staff_StaffId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Instrumentation_Machine_MachineEntityId",
                table: "Instrumentation");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "ZoneDeviceDriver");

            migrationBuilder.DropIndex(
                name: "IX_Instrumentation_MachineEntityId",
                table: "Instrumentation");

            migrationBuilder.DropIndex(
                name: "IX_Image_StaffId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ZoneHarvest");

            migrationBuilder.DropColumn(
                name: "IsProblem",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "IsProblem",
                table: "Instrumentation");

            migrationBuilder.DropColumn(
                name: "MachineEntityId",
                table: "Instrumentation");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DeviceDriver");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DeviceDriver");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Zone",
                newName: "DateCreateFarm");

            migrationBuilder.RenameColumn(
                name: "DateOfPurChanse",
                table: "Machine",
                newName: "DateStartedUsing");

            migrationBuilder.RenameColumn(
                name: "DateOfPurchanse",
                table: "Instrumentation",
                newName: "DateStartedUsing");

            migrationBuilder.RenameColumn(
                name: "DateOfPurChanse",
                table: "DeviceDriver",
                newName: "DateStartedUsing");

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Machine",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeviceDriverTypeId",
                table: "DeviceDriver",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAction",
                table: "DeviceDriver",
                type: "bit",
                nullable: true);

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

            migrationBuilder.AddColumn<int>(
                name: "ZoneId",
                table: "DeviceDriver",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeviceDriverType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDriverType", x => x.Id);
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

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDriver_DeviceDriverTypeId",
                table: "DeviceDriver",
                column: "DeviceDriverTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDriver_ZoneId",
                table: "DeviceDriver",
                column: "ZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceDriver_DeviceDriverType_DeviceDriverTypeId",
                table: "DeviceDriver",
                column: "DeviceDriverTypeId",
                principalTable: "DeviceDriverType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceDriver_Zone_ZoneId",
                table: "DeviceDriver",
                column: "ZoneId",
                principalTable: "Zone",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceDriver_DeviceDriverType_DeviceDriverTypeId",
                table: "DeviceDriver");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceDriver_Zone_ZoneId",
                table: "DeviceDriver");

            migrationBuilder.DropTable(
                name: "DeviceDriverType");

            migrationBuilder.DropTable(
                name: "JobInZone");

            migrationBuilder.DropIndex(
                name: "IX_DeviceDriver_DeviceDriverTypeId",
                table: "DeviceDriver");

            migrationBuilder.DropIndex(
                name: "IX_DeviceDriver_ZoneId",
                table: "DeviceDriver");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "DeviceDriverTypeId",
                table: "DeviceDriver");

            migrationBuilder.DropColumn(
                name: "IsAction",
                table: "DeviceDriver");

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

            migrationBuilder.DropColumn(
                name: "ZoneId",
                table: "DeviceDriver");

            migrationBuilder.RenameColumn(
                name: "DateCreateFarm",
                table: "Zone",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "DateStartedUsing",
                table: "Machine",
                newName: "DateOfPurChanse");

            migrationBuilder.RenameColumn(
                name: "DateStartedUsing",
                table: "Instrumentation",
                newName: "DateOfPurchanse");

            migrationBuilder.RenameColumn(
                name: "DateStartedUsing",
                table: "DeviceDriver",
                newName: "DateOfPurChanse");

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "ZoneHarvest",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProblem",
                table: "Machine",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProblem",
                table: "Instrumentation",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MachineEntityId",
                table: "Instrumentation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DeviceDriver",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DeviceDriver",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bonus = table.Column<int>(type: "int", nullable: true),
                    DateJoin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoteOfSalary = table.Column<int>(type: "int", nullable: true),
                    OffWorksOfMonth = table.Column<int>(type: "int", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: true),
                    WorksOfMonth = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZoneDeviceDriver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceDriverId = table.Column<int>(type: "int", nullable: true),
                    ZoneId = table.Column<int>(type: "int", nullable: true),
                    IsAuto = table.Column<bool>(type: "bit", nullable: true),
                    IsDaily = table.Column<bool>(type: "bit", nullable: true),
                    OpenTimer = table.Column<int>(type: "int", nullable: true),
                    ShutDownTime = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneDeviceDriver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZoneDeviceDriver_DeviceDriver_DeviceDriverId",
                        column: x => x.DeviceDriverId,
                        principalTable: "DeviceDriver",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ZoneDeviceDriver_Zone_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zone",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instrumentation_MachineEntityId",
                table: "Instrumentation",
                column: "MachineEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_StaffId",
                table: "Image",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneDeviceDriver_DeviceDriverId",
                table: "ZoneDeviceDriver",
                column: "DeviceDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneDeviceDriver_ZoneId",
                table: "ZoneDeviceDriver",
                column: "ZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Staff_StaffId",
                table: "Image",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instrumentation_Machine_MachineEntityId",
                table: "Instrumentation",
                column: "MachineEntityId",
                principalTable: "Machine",
                principalColumn: "Id");
        }
    }
}
