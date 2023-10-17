using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class addaddressUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8f9b8173-1ede-4f54-83aa-5c8858904d11");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "eef7b0cb-2626-4e7d-b0fc-84b58151f8cd");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "171c5abc-d4e6-49a4-8b8d-1d098722e626", null, "Administrator", "ADMINISTRATOR" },
                    { "bb2e40ce-a56b-4bea-b3e8-e1e6399efb67", null, "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "171c5abc-d4e6-49a4-8b8d-1d098722e626");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "bb2e40ce-a56b-4bea-b3e8-e1e6399efb67");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8f9b8173-1ede-4f54-83aa-5c8858904d11", null, "Manager", "MANAGER" },
                    { "eef7b0cb-2626-4e7d-b0fc-84b58151f8cd", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
