using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class Updatedatabase202309242 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Machine",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "IsProblem",
                table: "Machine",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Instrumentation",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "IsProblem",
                table: "Instrumentation",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProblem",
                table: "DeviceDriver",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProblem",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "IsProblem",
                table: "Instrumentation");

            migrationBuilder.DropColumn(
                name: "IsProblem",
                table: "DeviceDriver");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Machine",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Instrumentation",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
