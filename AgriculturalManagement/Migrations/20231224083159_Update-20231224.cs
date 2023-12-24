using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class Update20231224 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ThresholdValueOn",
                table: "DeviceInstrumentThreshold",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ThresholdValueOff",
                table: "DeviceInstrumentThreshold",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ThresholdValueOn",
                table: "DeviceInstrumentThreshold",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ThresholdValueOff",
                table: "DeviceInstrumentThreshold",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
