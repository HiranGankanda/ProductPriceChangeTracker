using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PPCT.DataAccessLayer.Migrations
{
    public partial class Update03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyMarginRecords",
                columns: table => new
                {
                    CompanyMarginRecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    SpecialRetailPrice = table.Column<double>(type: "float", nullable: false),
                    CompanyMargin = table.Column<double>(type: "float", nullable: false),
                    WithVAT = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Bouns = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMarginRecords", x => x.CompanyMarginRecordID);
                });

            migrationBuilder.CreateTable(
                name: "CompanyMarginRecords_History",
                columns: table => new
                {
                    HistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyMarginRecordID = table.Column<int>(type: "int", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    SpecialRetailPrice = table.Column<double>(type: "float", nullable: false),
                    CompanyMargin = table.Column<double>(type: "float", nullable: false),
                    WithVAT = table.Column<bool>(type: "bit", nullable: false),
                    Bouns = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    ChangeNote = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    ChangedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ChangedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMarginRecords_History", x => x.HistoryID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyMarginRecords");

            migrationBuilder.DropTable(
                name: "CompanyMarginRecords_History");
        }
    }
}
