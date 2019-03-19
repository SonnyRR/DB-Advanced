using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesMapping.Data.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "BirthDay" },
                values: new object[] { "Neznam", new DateTime(2019, 3, 4, 14, 41, 5, 79, DateTimeKind.Local).AddTicks(2481) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "Address",
                value: "Neznam2");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "BirthDay",
                value: new DateTime(2018, 3, 19, 14, 41, 5, 81, DateTimeKind.Local).AddTicks(4307));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "BirthDay" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "Address",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "BirthDay",
                value: null);
        }
    }
}
