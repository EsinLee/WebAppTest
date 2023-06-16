using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppTest.Migrations
{
    /// <inheritdoc />
    public partial class MemberUpdate_AccountingStartUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountNamess",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNamess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CashFlows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProposerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiptId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashFlows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Money = table.Column<double>(type: "float", nullable: false),
                    Trader = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNamesId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountNamess");

            migrationBuilder.DropTable(
                name: "CashFlows");

            migrationBuilder.DropTable(
                name: "Receipts");
        }
    }
}
