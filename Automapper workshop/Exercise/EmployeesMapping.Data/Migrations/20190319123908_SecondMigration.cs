using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesMapping.Data.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "BirthDay", "FirstName", "LastName", "Salary" },
                values: new object[,]
                {
                    { 1, null, null, "Georgi", "Georgiev", 12131.44m },
                    { 2, null, null, "Maria", "Marieva", 999.10m },
                    { 3, null, null, "Alisia", "Alisieva", 11111.11m },
                    { 4, null, null, "Pesho", "Peshov", 431.44m },
                    { 5, null, null, "Vyara", "Marinova", 2000.44m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
