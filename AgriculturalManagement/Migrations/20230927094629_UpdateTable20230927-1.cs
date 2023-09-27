using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable202309271 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "8e533c0b-d0fa-4a96-a274-02298b418217", null, "Administrator", "ADMINISTRATOR" },
                    { "ae85d355-a9e2-4717-9e72-ebd644f7158d", null, "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8e533c0b-d0fa-4a96-a274-02298b418217");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ae85d355-a9e2-4717-9e72-ebd644f7158d");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a16ab323-39f5-4b7f-80de-ac34ae22a208", null, "Administrator", "ADMINISTRATOR" },
                    { "cef8100c-9def-41c1-a48c-1349b4e93780", null, "Manager", "MANAGER" }
                });
        }
    }
}
