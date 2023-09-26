using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class CreateDataForTreeType1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "22ed9609-72b4-4f9d-b884-6f918ba4a31a");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a1e9ab96-3303-427b-9248-fcb8c8360f50");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a16ab323-39f5-4b7f-80de-ac34ae22a208", null, "Administrator", "ADMINISTRATOR" },
                    { "cef8100c-9def-41c1-a48c-1349b4e93780", null, "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a16ab323-39f5-4b7f-80de-ac34ae22a208");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "cef8100c-9def-41c1-a48c-1349b4e93780");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "22ed9609-72b4-4f9d-b884-6f918ba4a31a", null, "Administrator", "ADMINISTRATOR" },
                    { "a1e9ab96-3303-427b-9248-fcb8c8360f50", null, "Manager", "MANAGER" }
                });
        }
    }
}
