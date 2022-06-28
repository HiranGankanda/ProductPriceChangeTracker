using Microsoft.EntityFrameworkCore.Migrations;

namespace PPCT.DataSupport.Migrations
{
    public partial class Update10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PLU",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandID",
                table: "Products",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PLU",
                table: "Products",
                type: "int",
                nullable: true,
                defaultValue: 0);
        }
    }
}
