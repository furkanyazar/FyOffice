using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Update_Employee_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UK_Employees_ComputerId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ComputerId",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComputerId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "UK_Employees_ComputerId",
                table: "Employees",
                column: "ComputerId",
                unique: true,
                filter: "[ComputerId] IS NOT NULL");
        }
    }
}