using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class Updatedatabase202309252 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Machine_MachineId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Users_UserId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_MachineId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_UserId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "Image",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "Image",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Image",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_MachineId",
                table: "Image",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_UserId",
                table: "Image",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Machine_MachineId",
                table: "Image",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Users_UserId",
                table: "Image",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
