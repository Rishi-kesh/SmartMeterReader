using Microsoft.EntityFrameworkCore.Migrations;

namespace TestingSmart.Migrations
{
    public partial class unitischanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Units",
                table: "Units");

            migrationBuilder.AddColumn<int>(
                name: "Unites",
                table: "Units",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unites",
                table: "Units");

            migrationBuilder.AddColumn<int>(
                name: "Units",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
