using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable20231024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9432e467-aa3a-4c0e-bfa3-e1d94bedd37a");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e17f0f08-dd77-4e59-9684-e0871fa0f61c");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "Instrumentation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "I");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Esp",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Esp",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "ClientId");

            migrationBuilder.AddColumn<int>(
                name: "MqttPort",
                table: "Esp",
                type: "int",
                nullable: false,
                defaultValue: 1883);

            migrationBuilder.AddColumn<string>(
                name: "MqttServer",
                table: "Esp",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "1bae7e9a-6bb6-455b-b84c-ea3bd4b51937");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Esp",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "public");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Esp",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "emqx");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "DeviceDriver",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "D");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7005dc2b-3ce5-4f8d-9447-528909adc686", null, "Administrator", "ADMINISTRATOR" },
                    { "ef61575c-391c-48fc-824e-c55b822701e9", null, "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7005dc2b-3ce5-4f8d-9447-528909adc686");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ef61575c-391c-48fc-824e-c55b822701e9");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "Instrumentation");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Esp");

            migrationBuilder.DropColumn(
                name: "MqttPort",
                table: "Esp");

            migrationBuilder.DropColumn(
                name: "MqttServer",
                table: "Esp");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Esp");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Esp");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "DeviceDriver");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Esp",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9432e467-aa3a-4c0e-bfa3-e1d94bedd37a", null, "Manager", "MANAGER" },
                    { "e17f0f08-dd77-4e59-9684-e0871fa0f61c", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
