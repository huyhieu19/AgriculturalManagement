using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class Updatedatabase202309253 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstrumentationTypeId",
                table: "Instrumentation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InstrumentationType",
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
                    table.PrimaryKey("PK_InstrumentationType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d152a596-7d69-4f29-b5b7-09d4d723f8c5", null, "Manager", "MANAGER" },
                    { "d64c9884-e29e-4b40-882e-9e70d3537e40", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instrumentation_InstrumentationTypeId",
                table: "Instrumentation",
                column: "InstrumentationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instrumentation_InstrumentationType_InstrumentationTypeId",
                table: "Instrumentation",
                column: "InstrumentationTypeId",
                principalTable: "InstrumentationType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instrumentation_InstrumentationType_InstrumentationTypeId",
                table: "Instrumentation");

            migrationBuilder.DropTable(
                name: "InstrumentationType");

            migrationBuilder.DropIndex(
                name: "IX_Instrumentation_InstrumentationTypeId",
                table: "Instrumentation");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d152a596-7d69-4f29-b5b7-09d4d723f8c5");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d64c9884-e29e-4b40-882e-9e70d3537e40");

            migrationBuilder.DropColumn(
                name: "InstrumentationTypeId",
                table: "Instrumentation");
        }
    }
}
