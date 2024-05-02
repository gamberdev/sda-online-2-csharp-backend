using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class creatertable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_item_products_ProductId1",
                table: "order_item");

            migrationBuilder.DropIndex(
                name: "IX_order_item_ProductId1",
                table: "order_item");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "order_item");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                table: "order_item",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_item_ProductId1",
                table: "order_item",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_products_ProductId1",
                table: "order_item",
                column: "ProductId1",
                principalTable: "products",
                principalColumn: "product_id");
        }
    }
}
