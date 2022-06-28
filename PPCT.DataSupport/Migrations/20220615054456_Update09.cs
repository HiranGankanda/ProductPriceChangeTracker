using Microsoft.EntityFrameworkCore.Migrations;

namespace PPCT.DataSupport.Migrations
{
    public partial class Update09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VATPercentages_History",
                table: "VATPercentages_History");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VATPercentages",
                table: "VATPercentages");

            migrationBuilder.RenameTable(
                name: "VATPercentages_History",
                newName: "CompanyVATPercentages_History");

            migrationBuilder.RenameTable(
                name: "VATPercentages",
                newName: "CompanyVATPercentages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyVATPercentages_History",
                table: "CompanyVATPercentages_History",
                column: "HistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyVATPercentages",
                table: "CompanyVATPercentages",
                column: "VATPercentageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyVATPercentages_History",
                table: "CompanyVATPercentages_History");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyVATPercentages",
                table: "CompanyVATPercentages");

            migrationBuilder.RenameTable(
                name: "CompanyVATPercentages_History",
                newName: "VATPercentages_History");

            migrationBuilder.RenameTable(
                name: "CompanyVATPercentages",
                newName: "VATPercentages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VATPercentages_History",
                table: "VATPercentages_History",
                column: "HistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VATPercentages",
                table: "VATPercentages",
                column: "VATPercentageId");
        }
    }
}
