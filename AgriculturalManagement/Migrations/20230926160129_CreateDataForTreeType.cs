using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class CreateDataForTreeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 21);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "22ed9609-72b4-4f9d-b884-6f918ba4a31a", null, "Administrator", "ADMINISTRATOR" },
                    { "a1e9ab96-3303-427b-9248-fcb8c8360f50", null, "Manager", "MANAGER" }
                });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Loại cây ăn quả có thịnh hành tại Việt Nam.");

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Loại cây ăn quả thường thấy trong vườn nhà dân.", "Chuối" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Cây sầu riêng thường được trồng ở miền Nam Việt Nam.", "Sầu riêng" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Loại cây ăn quả có hạt lớn và ngon.", "Mít" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Vải là loại cây ăn quả có quả nhỏ màu đỏ tươi.", "Vải" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Nhãn là cây ăn quả thường thấy tại Việt Nam.", "Nhãn" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Loại cây ăn quả có vỏ màu đỏ và hạt trắng.", "Chôm chôm" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Bưởi là loại cây ăn quả có vị ngọt và hấp dẫn.", "Bưởi" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Cam là loại cây ăn quả chứa nhiều vitamin C.", "Cam" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Nho là loại cây ăn quả có nhiều loại khác nhau.", "Nho" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Đu đủ là cây ăn quả phổ biến ở Việt Nam.", "Đu đủ" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 12,
                column: "Description",
                value: "Khế là cây ăn quả có hương vị chua ngọt đặc trưng.");

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Loại cây ăn quả có hình dáng độc đáo.", "Thanh long" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Dứa là cây ăn quả thường thấy trong vườn nhà dân.", "Dứa" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Cây cau thường trồng ở vùng nhiệt đới Việt Nam.", "Cây cau" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Chanh là cây ăn quả có vị chua và mùi thơm.", "Chanh" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Me là cây ăn quả có vị chua ngọt đặc trưng.", "Me" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Sapoche là loại cây ăn quả có vị ngọt và mùi thơm đặc trưng.", "Sapoche" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Cherry là loại cây ăn quả có hạt nhỏ màu đỏ.", "Cherry" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "NameType" },
                values: new object[] { "Lựu là cây ăn quả có hình dáng đặc trưng.", "Lựu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "2f37d35a-166d-4f37-80bf-7839dfe9c7f0", null, "Administrator", "ADMINISTRATOR" },
                    { "859cacb6-4d02-4335-8402-5bb1cde8130b", null, "Manager", "MANAGER" }
                });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Thanh long" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Chuối" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Đu đủ" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Ổi" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Táo" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Nhãn" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Chôm chôm" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Vải" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Sầu riêng" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Mít" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 12,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Dứa" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Na" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Sapoche" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Me" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Cam" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Bưởi" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Chanh" });

            migrationBuilder.UpdateData(
                table: "TypeTree",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "NameType" },
                values: new object[] { null, "Chanh dây" });

            migrationBuilder.InsertData(
                table: "TypeTree",
                columns: new[] { "Id", "Description", "ImageUrl", "NameType", "Note" },
                values: new object[] { 21, null, null, "Nho", null });
        }
    }
}
