using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMHO.Migrations
{
    public partial class ImagemodelchangedFileNameFormatcolumnsaddedandUricolumnremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uri",
                table: "Images",
                newName: "FileName");

            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "Images",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 2,
                column: "Format",
                value: "jpeg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Format",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Images",
                newName: "Uri");
        }
    }
}
