using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Web.Data.Migrations
{
    public partial class ImagePaths : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePaths",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviewImagePath",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePaths",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PreviewImagePath",
                table: "Products");
        }
    }
}
