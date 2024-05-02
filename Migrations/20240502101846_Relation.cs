using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_orderItem_OrderId",
                table: "orderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_orderItem_ProductId",
                table: "orderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_orderItem_UserId",
                table: "orderItem",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItem_order_OrderId",
                table: "orderItem",
                column: "OrderId",
                principalTable: "order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orderItem_products_ProductId",
                table: "orderItem",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orderItem_users_UserId",
                table: "orderItem",
                column: "UserId",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderItem_order_OrderId",
                table: "orderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_orderItem_products_ProductId",
                table: "orderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_orderItem_users_UserId",
                table: "orderItem");

            migrationBuilder.DropIndex(
                name: "IX_orderItem_OrderId",
                table: "orderItem");

            migrationBuilder.DropIndex(
                name: "IX_orderItem_ProductId",
                table: "orderItem");

            migrationBuilder.DropIndex(
                name: "IX_orderItem_UserId",
                table: "orderItem");
        }
    }
}
