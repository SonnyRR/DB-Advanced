using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesMapping.Data.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Employees",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "BirthDay",
                value: new DateTime(2019, 3, 6, 9, 41, 38, 779, DateTimeKind.Local).AddTicks(4225));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "BirthDay",
                value: new DateTime(2018, 3, 21, 9, 41, 38, 781, DateTimeKind.Local).AddTicks(8290));

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "BirthDay",
                value: new DateTime(2019, 3, 4, 16, 6, 30, 351, DateTimeKind.Local).AddTicks(1308));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "BirthDay",
                value: new DateTime(2018, 3, 19, 16, 6, 30, 353, DateTimeKind.Local).AddTicks(4074));
        }
    }
}
