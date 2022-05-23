using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopifyBackend.Migrations
{
    public partial class Edit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Relationships_InventoryId",
                table: "Relationships",
                column: "InventoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Inventory_InventoryId",
                table: "Relationships",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Inventory_InventoryId",
                table: "Relationships");

            migrationBuilder.DropIndex(
                name: "IX_Relationships_InventoryId",
                table: "Relationships");
        }
    }
}
