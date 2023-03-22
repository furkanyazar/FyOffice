using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Update_Computer_Unique_Keys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UK_Computers_LicenceKey",
                table: "Computers");

            migrationBuilder.AlterColumn<string>(
                name: "LicenceKey",
                table: "Computers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LicenceKey",
                table: "Computers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "UK_Computers_LicenceKey",
                table: "Computers",
                column: "LicenceKey",
                unique: true,
                filter: "[LicenceKey] IS NOT NULL");
        }
    }
}
