using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PPCT.DataAccessLayer.Migrations
{
    public partial class Update12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Companies_History");

            migrationBuilder.DropTable(
                name: "CompanyMarginRecords");

            migrationBuilder.DropTable(
                name: "CompanyMarginRecords_History");

            migrationBuilder.DropTable(
                name: "CompanyVATPercentages");

            migrationBuilder.DropTable(
                name: "CompanyVATPercentages_History");

            migrationBuilder.CreateTable(
                name: "RetailStore",
                columns: table => new
                {
                    RetailStoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetailStoreName = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailStore", x => x.RetailStoreId);
                });

            migrationBuilder.CreateTable(
                name: "RetailStore_History",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetailStoreId = table.Column<int>(type: "int", nullable: true),
                    RetailStoreName = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ChangeNote = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailStore_History", x => x.HistoryId);
                });

            migrationBuilder.CreateTable(
                name: "RetailStoreMarginRecords",
                columns: table => new
                {
                    RetailStoreMarginRecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetailStoreID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    SpecialRetailPrice = table.Column<double>(type: "float", nullable: false),
                    RetailStoreMargin = table.Column<double>(type: "float", nullable: false),
                    WithVAT = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Bouns = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailStoreMarginRecords", x => x.RetailStoreMarginRecordID);
                });

            migrationBuilder.CreateTable(
                name: "RetailStoreMarginRecords_History",
                columns: table => new
                {
                    HistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetailStoreMarginRecordID = table.Column<int>(type: "int", nullable: false),
                    RetailStoreID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    SpecialRetailPrice = table.Column<double>(type: "float", nullable: false),
                    RetailStoreMargin = table.Column<double>(type: "float", nullable: false),
                    WithVAT = table.Column<bool>(type: "bit", nullable: false),
                    Bouns = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    ChangeNote = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    ChangedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ChangedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailStoreMarginRecords_History", x => x.HistoryID);
                });

            migrationBuilder.CreateTable(
                name: "RetailStoreVATPercentages",
                columns: table => new
                {
                    VATPercentageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetailStoreID = table.Column<int>(type: "int", nullable: false),
                    VATPercentageValue = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailStoreVATPercentages", x => x.VATPercentageId);
                });

            migrationBuilder.CreateTable(
                name: "RetailStoreVATPercentages_History",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VATPercentageId = table.Column<int>(type: "int", nullable: true),
                    RetailStoreID = table.Column<int>(type: "int", nullable: true),
                    VATPercentageValue = table.Column<double>(type: "float", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ChangeNote = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailStoreVATPercentages_History", x => x.HistoryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RetailStore");

            migrationBuilder.DropTable(
                name: "RetailStore_History");

            migrationBuilder.DropTable(
                name: "RetailStoreMarginRecords");

            migrationBuilder.DropTable(
                name: "RetailStoreMarginRecords_History");

            migrationBuilder.DropTable(
                name: "RetailStoreVATPercentages");

            migrationBuilder.DropTable(
                name: "RetailStoreVATPercentages_History");

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Companies_History",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangeNote = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CompanyName = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies_History", x => x.HistoryId);
                });

            migrationBuilder.CreateTable(
                name: "CompanyMarginRecords",
                columns: table => new
                {
                    CompanyMarginRecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bouns = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    CompanyMargin = table.Column<double>(type: "float", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    SpecialRetailPrice = table.Column<double>(type: "float", nullable: false),
                    WithVAT = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
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
                    Bouns = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    ChangeNote = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    ChangedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ChangedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    CompanyMargin = table.Column<double>(type: "float", nullable: false),
                    CompanyMarginRecordID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    SpecialRetailPrice = table.Column<double>(type: "float", nullable: false),
                    WithVAT = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMarginRecords_History", x => x.HistoryID);
                });

            migrationBuilder.CreateTable(
                name: "CompanyVATPercentages",
                columns: table => new
                {
                    VATPercentageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    VATPercentageValue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyVATPercentages", x => x.VATPercentageId);
                });

            migrationBuilder.CreateTable(
                name: "CompanyVATPercentages_History",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangeNote = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    VATPercentageId = table.Column<int>(type: "int", nullable: true),
                    VATPercentageValue = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyVATPercentages_History", x => x.HistoryId);
                });
        }
    }
}
