using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Retail_Bank_Management.Migrations
{
    public partial class AccountsMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountID", "AccountType", "Balance", "CustomerID", "LastUpdated", "Message", "Status" },
                values: new object[] { 203206016, "Savings", 5000L, 100000000, new DateTime(2020, 7, 27, 16, 33, 23, 582, DateTimeKind.Local).AddTicks(9206), "Account Created", "Active" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountID",
                keyValue: 203206016);
        }
    }
}
