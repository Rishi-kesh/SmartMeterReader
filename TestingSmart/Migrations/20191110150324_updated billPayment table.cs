using Microsoft.EntityFrameworkCore.Migrations;

namespace TestingSmart.Migrations
{
    public partial class updatedbillPaymenttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_BillAmount_BillAmountId",
                table: "BillPayment");

            migrationBuilder.DropIndex(
                name: "IX_BillPayment_BillAmountId",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "AmountToBePaid",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "BillAmountId",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "Due",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "OwnerEsewaId",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "UserEsewaId",
                table: "BillPayment");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "BillPayment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BillPayment",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BillPayment");

            migrationBuilder.AddColumn<int>(
                name: "AmountPaid",
                table: "BillPayment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AmountToBePaid",
                table: "BillPayment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BillAmountId",
                table: "BillPayment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Due",
                table: "BillPayment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsPaid",
                table: "BillPayment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "BillPayment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OwnerEsewaId",
                table: "BillPayment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEsewaId",
                table: "BillPayment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillPayment_BillAmountId",
                table: "BillPayment",
                column: "BillAmountId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayment_BillAmount_BillAmountId",
                table: "BillPayment",
                column: "BillAmountId",
                principalTable: "BillAmount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
