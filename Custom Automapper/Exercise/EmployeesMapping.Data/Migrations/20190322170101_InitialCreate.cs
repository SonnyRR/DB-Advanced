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
                    Address = table.Column<string>(nullable: true),
                    ManagerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "BirthDay", "FirstName", "LastName", "ManagerId", "Salary" },
                values: new object[,]
                {
                    { 1, null, null, "Georgi", "Georgiev", null, 12131.44m },
                    { 2, "Neznam", new DateTime(2019, 3, 7, 19, 1, 1, 170, DateTimeKind.Local).AddTicks(4017), "Maria", "Marieva", null, 999.10m },
                    { 3, null, null, "Alisia", "Alisieva", null, 11111.11m },
                    { 4, "Neznam2", null, "Pesho", "Peshov", null, 431.44m },
                    { 6, null, new DateTime(2018, 3, 22, 19, 1, 1, 172, DateTimeKind.Local).AddTicks(6247), "Miro", "Mirov", null, 2000.44m },
                    { 7, null, new DateTime(2011, 1, 3, 19, 1, 1, 172, DateTimeKind.Local).AddTicks(6264), "Blago", "Petkov", null, 2000.44m },
                    { 8, null, new DateTime(2008, 4, 8, 19, 1, 1, 172, DateTimeKind.Local).AddTicks(6269), "Emanuela", "Marinova", null, 2000.44m },
                    { 9, null, new DateTime(2013, 9, 29, 19, 1, 1, 172, DateTimeKind.Local).AddTicks(6273), "Koce", "Kocev", null, 2000.44m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
