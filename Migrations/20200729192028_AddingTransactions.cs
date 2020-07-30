using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Retail_Bank_Management.Migrations
{
    public partial class AddingTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountID",
                keyValue: 203206016,
                column: "LastUpdated",
                value: new DateTime(2020, 7, 30, 0, 50, 27, 397, DateTimeKind.Local).AddTicks(9637));

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionID", "AccountID", "Amount", "Description", "TransactionDate" },
                values: new object[] { 111555000, 203206016, 1000, "Deposit", new DateTime(2020, 7, 30, 0, 50, 27, 399, DateTimeKind.Local).AddTicks(6373) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountID",
                keyValue: 203206016,
                column: "LastUpdated",
                value: new DateTime(2020, 7, 29, 12, 16, 20, 861, DateTimeKind.Local).AddTicks(7110));
        }
    }
}
