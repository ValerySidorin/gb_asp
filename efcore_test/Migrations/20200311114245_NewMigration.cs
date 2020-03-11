using Microsoft.EntityFrameworkCore.Migrations;

namespace efcore_test.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "FirstName", table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
