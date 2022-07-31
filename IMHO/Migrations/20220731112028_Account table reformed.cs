using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMHO.Migrations
{
    public partial class Accounttablereformed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Accounts",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Accounts",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Accounts",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Accounts",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Accounts",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NameIdentifier",
                table: "Accounts",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "Accounts",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "Accounts",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Accounts",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Mobile", "NameIdentifier", "Password", "Provider", "Roles", "Username" },
                values: new object[] { 1, "j@test.com", "Junhyeok", "Jang", "111-111-111", null, "h", "Cookies", "Admin", "j" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "UserId",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "NameIdentifier",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Accounts");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Password",
                keyValue: null,
                column: "Password",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Accounts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "LastName",
                keyValue: null,
                column: "LastName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Accounts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "FirstName",
                keyValue: null,
                column: "FirstName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Accounts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Accounts",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Accounts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Accounts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Accounts",
                type: "longblob",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mobile = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameIdentifier = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Provider = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Roles = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Mobile", "NameIdentifier", "Password", "Provider", "Roles", "Username" },
                values: new object[] { 1, "j@test.com", "Junhyeok", "Jang", "111-111-111", null, "h", "Cookies", "Admin", "j" });
        }
    }
}
