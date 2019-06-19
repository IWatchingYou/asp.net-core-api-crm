using Microsoft.EntityFrameworkCore.Migrations;

namespace crm.Migrations
{
    public partial class InitialTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_payments_CustomerId",
                table: "payments",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_customers_CustomerId",
                table: "payments",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_customers_CustomerId",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_CustomerId",
                table: "payments");
        }
    }
}
