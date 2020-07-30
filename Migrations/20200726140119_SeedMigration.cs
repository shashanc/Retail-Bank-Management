using Microsoft.EntityFrameworkCore.Migrations;

namespace Retail_Bank_Management.Migrations
{
    public partial class SeedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "Address", "Address2", "Age", "City", "Name", "SSNID", "State" },
                values: new object[] { 100000000, "S/S MQ 133", "Dhori Staff Quarters", (byte)23, "Bokaro", "Shashank Sharma", 123456789, "Jharkhand" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "Address", "Address2", "Age", "City", "Name", "SSNID", "State" },
                values: new object[] { 100000001, "MQ 123", "Kargali Staff Quarters", (byte)21, "Bokaro", "Nikhil Raj", 100456789, "Jharkhand" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "Address", "Address2", "Age", "City", "Name", "SSNID", "State" },
                values: new object[] { 100000002, "S/S SQ 24", "Dhori Staff Quarters", (byte)21, "Bokaro", "Adarsh Singh", 123459789, "Jharkhand" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 100000000);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 100000001);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 100000002);
        }
    }
}
