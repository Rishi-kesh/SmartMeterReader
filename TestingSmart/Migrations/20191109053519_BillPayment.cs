using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestingSmart.Migrations
{
    public partial class BillPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillAmount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    TotalUnit = table.Column<int>(nullable: false),
                    CurrentUnit = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillAmount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillAmount_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillPayment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillAmountId = table.Column<int>(nullable: false),
                    IsPaid = table.Column<int>(nullable: false),
                    AmountToBePaid = table.Column<int>(nullable: false),
                    AmountPaid = table.Column<int>(nullable: false),
                    Due = table.Column<int>(nullable: false),
                    UserEsewaId = table.Column<string>(nullable: true),
                    OwnerEsewaId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    MyProperty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillPayment_BillAmount_BillAmountId",
                        column: x => x.BillAmountId,
                        principalTable: "BillAmount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillAmount_UserId",
                table: "BillAmount",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BillPayment_BillAmountId",
                table: "BillPayment",
                column: "BillAmountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillPayment");

            migrationBuilder.DropTable(
                name: "BillAmount");
        }
    }
}
