using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class configColumn202311102 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Topic",
                table: "DeviceDriver");

            migrationBuilder.RenameColumn(
                name: "IsAction",
                table: "DeviceType",
                newName: "IsUsed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "DeviceType",
                newName: "IsAction");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "DeviceDriver",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "D");
        }
    }
}
