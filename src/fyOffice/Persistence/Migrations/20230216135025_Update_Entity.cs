using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Update_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Users",
                newName: "DateOfLastUpdate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Users",
                newName: "DateOfCreate");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "UserOperationClaims",
                newName: "DateOfLastUpdate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "UserOperationClaims",
                newName: "DateOfCreate");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "RefreshTokens",
                newName: "DateOfLastUpdate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "RefreshTokens",
                newName: "DateOfCreate");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "OperationClaims",
                newName: "DateOfLastUpdate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "OperationClaims",
                newName: "DateOfCreate");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Employees",
                newName: "DateOfLastUpdate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Employees",
                newName: "DateOfCreate");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Computers",
                newName: "DateOfLastUpdate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Computers",
                newName: "DateOfCreate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfLastUpdate",
                table: "Users",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "DateOfCreate",
                table: "Users",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "DateOfLastUpdate",
                table: "UserOperationClaims",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "DateOfCreate",
                table: "UserOperationClaims",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "DateOfLastUpdate",
                table: "RefreshTokens",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "DateOfCreate",
                table: "RefreshTokens",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "DateOfLastUpdate",
                table: "OperationClaims",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "DateOfCreate",
                table: "OperationClaims",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "DateOfLastUpdate",
                table: "Employees",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "DateOfCreate",
                table: "Employees",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "DateOfLastUpdate",
                table: "Computers",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "DateOfCreate",
                table: "Computers",
                newName: "CreatedDate");
        }
    }
}
