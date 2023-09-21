using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class createtable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_StaffEntities_StaffId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Instrumentation_MachineEntities_MachineEntityId",
                table: "Instrumentation");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineWarranlyDate_DeviceDriverEntities_DeviceDriversId",
                table: "MachineWarranlyDate");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineWarranlyDate_MachineEntities_MachineId",
                table: "MachineWarranlyDate");

            migrationBuilder.DropForeignKey(
                name: "FK_Zone_TypeTreeEntities_TypeTreeId",
                table: "Zone");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneDeviceDriver_DeviceDriverEntities_DeviceDriverId",
                table: "ZoneDeviceDriver");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeTreeEntities",
                table: "TypeTreeEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffEntities",
                table: "StaffEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MachineEntities",
                table: "MachineEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceDriverEntities",
                table: "DeviceDriverEntities");

            migrationBuilder.RenameTable(
                name: "TypeTreeEntities",
                newName: "TypeTree");

            migrationBuilder.RenameTable(
                name: "StaffEntities",
                newName: "Staff");

            migrationBuilder.RenameTable(
                name: "MachineEntities",
                newName: "Machine");

            migrationBuilder.RenameTable(
                name: "DeviceDriverEntities",
                newName: "DeviceDriver");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeTree",
                table: "TypeTree",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                table: "Staff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Machine",
                table: "Machine",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceDriver",
                table: "DeviceDriver",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MachineWarranlyDate_DeviceDriver_DeviceDriversId",
                table: "MachineWarranlyDate",
                column: "DeviceDriversId",
                principalTable: "DeviceDriver",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineWarranlyDate_Machine_MachineId",
                table: "MachineWarranlyDate",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Zone_TypeTree_TypeTreeId",
                table: "Zone",
                column: "TypeTreeId",
                principalTable: "TypeTree",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneDeviceDriver_DeviceDriver_DeviceDriverId",
                table: "ZoneDeviceDriver",
                column: "DeviceDriverId",
                principalTable: "DeviceDriver",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Staff_StaffId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Instrumentation_Machine_MachineEntityId",
                table: "Instrumentation");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineWarranlyDate_DeviceDriver_DeviceDriversId",
                table: "MachineWarranlyDate");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineWarranlyDate_Machine_MachineId",
                table: "MachineWarranlyDate");

            migrationBuilder.DropForeignKey(
                name: "FK_Zone_TypeTree_TypeTreeId",
                table: "Zone");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneDeviceDriver_DeviceDriver_DeviceDriverId",
                table: "ZoneDeviceDriver");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeTree",
                table: "TypeTree");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff",
                table: "Staff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Machine",
                table: "Machine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceDriver",
                table: "DeviceDriver");

            migrationBuilder.RenameTable(
                name: "TypeTree",
                newName: "TypeTreeEntities");

            migrationBuilder.RenameTable(
                name: "Staff",
                newName: "StaffEntities");

            migrationBuilder.RenameTable(
                name: "Machine",
                newName: "MachineEntities");

            migrationBuilder.RenameTable(
                name: "DeviceDriver",
                newName: "DeviceDriverEntities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeTreeEntities",
                table: "TypeTreeEntities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffEntities",
                table: "StaffEntities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MachineEntities",
                table: "MachineEntities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceDriverEntities",
                table: "DeviceDriverEntities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_StaffEntities_StaffId",
                table: "Image",
                column: "StaffId",
                principalTable: "StaffEntities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instrumentation_MachineEntities_MachineEntityId",
                table: "Instrumentation",
                column: "MachineEntityId",
                principalTable: "MachineEntities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineWarranlyDate_DeviceDriverEntities_DeviceDriversId",
                table: "MachineWarranlyDate",
                column: "DeviceDriversId",
                principalTable: "DeviceDriverEntities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineWarranlyDate_MachineEntities_MachineId",
                table: "MachineWarranlyDate",
                column: "MachineId",
                principalTable: "MachineEntities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Zone_TypeTreeEntities_TypeTreeId",
                table: "Zone",
                column: "TypeTreeId",
                principalTable: "TypeTreeEntities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneDeviceDriver_DeviceDriverEntities_DeviceDriverId",
                table: "ZoneDeviceDriver",
                column: "DeviceDriverId",
                principalTable: "DeviceDriverEntities",
                principalColumn: "Id");
        }
    }
}
