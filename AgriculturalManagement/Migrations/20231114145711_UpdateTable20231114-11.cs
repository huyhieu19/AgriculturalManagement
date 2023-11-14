using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriculturalManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable2023111411 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimerDeviceDriver_Device_DeviceDriverId",
                table: "TimerDeviceDriver");

            migrationBuilder.DropForeignKey(
                name: "FK_Zone_TypeTree_TypeTreeId",
                table: "Zone");

            migrationBuilder.DropTable(
                name: "TypeTree");

            migrationBuilder.DropIndex(
                name: "IX_Zone_TypeTreeId",
                table: "Zone");

            migrationBuilder.DropColumn(
                name: "IsAffected",
                table: "TimerDeviceDriver");

            migrationBuilder.DropColumn(
                name: "ResponseType",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "Device");

            migrationBuilder.RenameColumn(
                name: "DeviceDriverId",
                table: "TimerDeviceDriver",
                newName: "DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_TimerDeviceDriver_DeviceDriverId",
                table: "TimerDeviceDriver",
                newName: "IX_TimerDeviceDriver_DeviceId");

            migrationBuilder.RenameColumn(
                name: "Gpio",
                table: "Device",
                newName: "Gate");

            migrationBuilder.AddForeignKey(
                name: "FK_TimerDeviceDriver_Device_DeviceId",
                table: "TimerDeviceDriver",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimerDeviceDriver_Device_DeviceId",
                table: "TimerDeviceDriver");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "TimerDeviceDriver",
                newName: "DeviceDriverId");

            migrationBuilder.RenameIndex(
                name: "IX_TimerDeviceDriver_DeviceId",
                table: "TimerDeviceDriver",
                newName: "IX_TimerDeviceDriver_DeviceDriverId");

            migrationBuilder.RenameColumn(
                name: "Gate",
                table: "Device",
                newName: "Gpio");

            migrationBuilder.AddColumn<bool>(
                name: "IsAffected",
                table: "TimerDeviceDriver",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponseType",
                table: "Device",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Topic",
                table: "Device",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TypeTree",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTree", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TypeTree",
                columns: new[] { "Id", "Description", "ImageUrl", "NameType", "Note" },
                values: new object[,]
                {
                    { 1, "Loại cây ăn quả có thịnh hành tại Việt Nam.", null, "Xoài", null },
                    { 2, "Loại cây ăn quả thường thấy trong vườn nhà dân.", null, "Chuối", null },
                    { 3, "Cây sầu riêng thường được trồng ở miền Nam Việt Nam.", null, "Sầu riêng", null },
                    { 4, "Loại cây ăn quả có hạt lớn và ngon.", null, "Mít", null },
                    { 5, "Vải là loại cây ăn quả có quả nhỏ màu đỏ tươi.", null, "Vải", null },
                    { 6, "Nhãn là cây ăn quả thường thấy tại Việt Nam.", null, "Nhãn", null },
                    { 7, "Loại cây ăn quả có vỏ màu đỏ và hạt trắng.", null, "Chôm chôm", null },
                    { 8, "Bưởi là loại cây ăn quả có vị ngọt và hấp dẫn.", null, "Bưởi", null },
                    { 9, "Cam là loại cây ăn quả chứa nhiều vitamin C.", null, "Cam", null },
                    { 10, "Nho là loại cây ăn quả có nhiều loại khác nhau.", null, "Nho", null },
                    { 11, "Đu đủ là cây ăn quả phổ biến ở Việt Nam.", null, "Đu đủ", null },
                    { 12, "Khế là cây ăn quả có hương vị chua ngọt đặc trưng.", null, "Khế", null },
                    { 13, "Loại cây ăn quả có hình dáng độc đáo.", null, "Thanh long", null },
                    { 14, "Dứa là cây ăn quả thường thấy trong vườn nhà dân.", null, "Dứa", null },
                    { 15, "Cây cau thường trồng ở vùng nhiệt đới Việt Nam.", null, "Cây cau", null },
                    { 16, "Chanh là cây ăn quả có vị chua và mùi thơm.", null, "Chanh", null },
                    { 17, "Me là cây ăn quả có vị chua ngọt đặc trưng.", null, "Me", null },
                    { 18, "Sapoche là loại cây ăn quả có vị ngọt và mùi thơm đặc trưng.", null, "Sapoche", null },
                    { 19, "Cherry là loại cây ăn quả có hạt nhỏ màu đỏ.", null, "Cherry", null },
                    { 20, "Lựu là cây ăn quả có hình dáng đặc trưng.", null, "Lựu", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zone_TypeTreeId",
                table: "Zone",
                column: "TypeTreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimerDeviceDriver_Device_DeviceDriverId",
                table: "TimerDeviceDriver",
                column: "DeviceDriverId",
                principalTable: "Device",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Zone_TypeTree_TypeTreeId",
                table: "Zone",
                column: "TypeTreeId",
                principalTable: "TypeTree",
                principalColumn: "Id");
        }
    }
}
