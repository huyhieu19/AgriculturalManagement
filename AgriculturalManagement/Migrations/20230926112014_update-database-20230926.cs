using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase20230926 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "68d02bac-0cd3-468b-b0bb-85211b10ad91");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "739b9215-01da-46d1-9eb0-e9d72eaed8ca");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TypeTree",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "InstrumentationType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "DeviceDriverType",
                columns: new[] { "Id", "Description", "ImageUrl", "Manufacturer", "Name" },
                values: new object[,]
                {
                    { 1, null, null, null, "Máy bơm" },
                    { 2, null, null, null, "Quạt gió" },
                    { 3, null, null, null, "Rèm cửa" }
                });

            migrationBuilder.InsertData(
                table: "InstrumentationType",
                columns: new[] { "Id", "Description", "ImageUrl", "Manufacturer", "Name", "Unit" },
                values: new object[,]
                {
                    { 1, null, null, null, "Cảm biến đo nhiệt độ", "*C" },
                    { 2, null, null, null, "Cảm biến đo độ ẩm không khí", "%" },
                    { 3, null, null, null, "Cảm biến nước mưa", null },
                    { 4, null, null, null, "Cảm biên gió", "Km/h" },
                    { 5, null, null, null, "Độ ẩm đất", "%" },
                    { 6, null, null, null, "Cảm biên đo độ PH của đất", "PH" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2f37d35a-166d-4f37-80bf-7839dfe9c7f0", null, "Administrator", "ADMINISTRATOR" },
                    { "859cacb6-4d02-4335-8402-5bb1cde8130b", null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "TypeTree",
                columns: new[] { "Id", "Description", "ImageUrl", "NameType", "Note" },
                values: new object[,]
                {
                    { 1, null, null, "Xoài", null },
                    { 2, null, null, "Thanh long", null },
                    { 3, null, null, "Chuối", null },
                    { 4, null, null, "Đu đủ", null },
                    { 5, null, null, "Ổi", null },
                    { 6, null, null, "Táo", null },
                    { 7, null, null, "Nhãn", null },
                    { 8, null, null, "Chôm chôm", null },
                    { 9, null, null, "Vải", null },
                    { 10, null, null, "Sầu riêng", null },
                    { 11, null, null, "Mít", null },
                    { 12, null, null, "Khế", null },
                    { 13, null, null, "Dứa", null },
                    { 14, null, null, "Na", null },
                    { 15, null, null, "Sapoche", null },
                    { 16, null, null, "Me", null },
                    { 17, null, null, "Cam", null },
                    { 18, null, null, "Bưởi", null },
                    { 19, null, null, "Chanh", null },
                    { 20, null, null, "Chanh dây", null },
                    { 21, null, null, "Nho", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeviceDriverType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeviceDriverType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DeviceDriverType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InstrumentationType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InstrumentationType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InstrumentationType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InstrumentationType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InstrumentationType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "InstrumentationType",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2f37d35a-166d-4f37-80bf-7839dfe9c7f0");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "859cacb6-4d02-4335-8402-5bb1cde8130b");

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TypeTree");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "InstrumentationType");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "68d02bac-0cd3-468b-b0bb-85211b10ad91", null, "Manager", "MANAGER" },
                    { "739b9215-01da-46d1-9eb0-e9d72eaed8ca", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
