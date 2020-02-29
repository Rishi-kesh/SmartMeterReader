using Microsoft.EntityFrameworkCore.Migrations;

namespace TestingSmart.Migrations
{
    public partial class unittablechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalcUnit",
                table: "Units",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentUnit",
                table: "Units",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Units",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BillPayment",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalcUnit",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "CurrentUnit",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Units");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BillPayment",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
