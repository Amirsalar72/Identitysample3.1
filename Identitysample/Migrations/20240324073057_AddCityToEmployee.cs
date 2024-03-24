using Microsoft.EntityFrameworkCore.Migrations;

namespace Identitysample.Migrations
{
    public partial class AddCityToEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "Employees");
        }
    }
}
