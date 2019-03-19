using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesMapping.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Salary = table.Column<decimal>(nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "BirthDay", "FirstName", "LastName", "Salary" },
                values: new object[,]
                {
                    { 1, null, null, "Georgi", "Georgiev", 12131.44m },
                    { 2, "Neznam", new DateTime(2019, 3, 4, 16, 6, 30, 351, DateTimeKind.Local).AddTicks(1308), "Maria", "Marieva", 999.10m },
                    { 3, null, null, "Alisia", "Alisieva", 11111.11m },
                    { 4, "Neznam2", null, "Pesho", "Peshov", 431.44m },
                    { 5, null, new DateTime(2018, 3, 19, 16, 6, 30, 353, DateTimeKind.Local).AddTicks(4074), "Vyara", "Marinova", 2000.44m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
