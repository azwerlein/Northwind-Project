using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Northwind.Migrations
{
    public partial class FixReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "String",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "ReviewText",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewText",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "String",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
