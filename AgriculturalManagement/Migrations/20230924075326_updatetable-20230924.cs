using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class updatetable20230924 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HarvestTime",
                table: "Zone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Zone",
                newName: "ZoneName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TypeTree",
                newName: "NameType");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Image",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZoneHarvestId",
                table: "Image",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ZoneHarvest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HarvertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZoneId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneHarvest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZoneHarvest_Zone_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zone",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_ZoneHarvestId",
                table: "Image",
                column: "ZoneHarvestId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneHarvest_ZoneId",
                table: "ZoneHarvest",
                column: "ZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_ZoneHarvest_ZoneHarvestId",
                table: "Image",
                column: "ZoneHarvestId",
                principalTable: "ZoneHarvest",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_ZoneHarvest_ZoneHarvestId",
                table: "Image");

            migrationBuilder.DropTable(
                name: "ZoneHarvest");

            migrationBuilder.DropIndex(
                name: "IX_Image_ZoneHarvestId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "ZoneHarvestId",
                table: "Image");

            migrationBuilder.RenameColumn(
                name: "ZoneName",
                table: "Zone",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameType",
                table: "TypeTree",
                newName: "Name");

            migrationBuilder.AddColumn<DateTime>(
                name: "HarvestTime",
                table: "Zone",
                type: "datetime2",
                nullable: true);
        }
    }
}
