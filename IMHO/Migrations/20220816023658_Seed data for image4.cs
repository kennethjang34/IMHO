using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMHO.Migrations
{
    public partial class Seeddataforimage4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Image",
                keyColumn: "ImageId",
                keyValue: 0);

            migrationBuilder.InsertData(
                table: "Image",
                columns: new[] { "ImageId", "Caption", "PostId", "Uri" },
                values: new object[] { 1, "test image", null, "/Users/JANG/IMHO/IMHO/Resources/Images/seol.jpeg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Image",
                keyColumn: "ImageId",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Image",
                columns: new[] { "ImageId", "Caption", "PostId", "Uri" },
                values: new object[] { 0, "test image", null, "/Users/JANG/IMHO/IMHO/Resources/Images/seol.jpeg" });
        }
    }
}
