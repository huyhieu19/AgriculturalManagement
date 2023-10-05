using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class update202310052 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceInstrumentOnOffEntity_DeviceDriver_DeviceDriverId",
                table: "DeviceInstrumentOnOffEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceInstrumentOnOffEntity_Instrumentation_InstrumentationId",
                table: "DeviceInstrumentOnOffEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceInstrumentOnOffEntity",
                table: "DeviceInstrumentOnOffEntity");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "15fb6761-a08d-44e8-b095-1dec117c7d54");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "628f0ce7-ab09-44ae-bbeb-8ae8a03174ac");

            migrationBuilder.RenameTable(
                name: "DeviceInstrumentOnOffEntity",
                newName: "DeviceInstrumentOnOff");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceInstrumentOnOffEntity_InstrumentationId",
                table: "DeviceInstrumentOnOff",
                newName: "IX_DeviceInstrumentOnOff_InstrumentationId");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceInstrumentOnOffEntity_DeviceDriverId",
                table: "DeviceInstrumentOnOff",
                newName: "IX_DeviceInstrumentOnOff_DeviceDriverId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "DeviceInstrumentOnOff",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceInstrumentOnOff",
                table: "DeviceInstrumentOnOff",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8f9b8173-1ede-4f54-83aa-5c8858904d11", null, "Manager", "MANAGER" },
                    { "eef7b0cb-2626-4e7d-b0fc-84b58151f8cd", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceInstrumentOnOff_DeviceDriver_DeviceDriverId",
                table: "DeviceInstrumentOnOff",
                column: "DeviceDriverId",
                principalTable: "DeviceDriver",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceInstrumentOnOff_Instrumentation_InstrumentationId",
                table: "DeviceInstrumentOnOff",
                column: "InstrumentationId",
                principalTable: "Instrumentation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceInstrumentOnOff_DeviceDriver_DeviceDriverId",
                table: "DeviceInstrumentOnOff");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceInstrumentOnOff_Instrumentation_InstrumentationId",
                table: "DeviceInstrumentOnOff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceInstrumentOnOff",
                table: "DeviceInstrumentOnOff");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8f9b8173-1ede-4f54-83aa-5c8858904d11");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "eef7b0cb-2626-4e7d-b0fc-84b58151f8cd");

            migrationBuilder.RenameTable(
                name: "DeviceInstrumentOnOff",
                newName: "DeviceInstrumentOnOffEntity");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceInstrumentOnOff_InstrumentationId",
                table: "DeviceInstrumentOnOffEntity",
                newName: "IX_DeviceInstrumentOnOffEntity_InstrumentationId");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceInstrumentOnOff_DeviceDriverId",
                table: "DeviceInstrumentOnOffEntity",
                newName: "IX_DeviceInstrumentOnOffEntity_DeviceDriverId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "DeviceInstrumentOnOffEntity",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceInstrumentOnOffEntity",
                table: "DeviceInstrumentOnOffEntity",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15fb6761-a08d-44e8-b095-1dec117c7d54", null, "Manager", "MANAGER" },
                    { "628f0ce7-ab09-44ae-bbeb-8ae8a03174ac", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceInstrumentOnOffEntity_DeviceDriver_DeviceDriverId",
                table: "DeviceInstrumentOnOffEntity",
                column: "DeviceDriverId",
                principalTable: "DeviceDriver",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceInstrumentOnOffEntity_Instrumentation_InstrumentationId",
                table: "DeviceInstrumentOnOffEntity",
                column: "InstrumentationId",
                principalTable: "Instrumentation",
                principalColumn: "Id");
        }
    }
}
