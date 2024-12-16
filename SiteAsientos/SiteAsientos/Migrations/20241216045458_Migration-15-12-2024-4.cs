using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteAsientos.Migrations
{
    /// <inheritdoc />
    public partial class Migration151220244 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Design_Orders_Order_Id",
                table: "Design");

            migrationBuilder.DropIndex(
                name: "IX_Design_Order_Id",
                table: "Design");

            migrationBuilder.DropColumn(
                name: "Order_Id",
                table: "Design");

            migrationBuilder.AddColumn<int>(
                name: "Order_DesignId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Order_DesignId",
                table: "Orders",
                column: "Order_DesignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Design_Order_DesignId",
                table: "Orders",
                column: "Order_DesignId",
                principalTable: "Design",
                principalColumn: "Design_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Design_Order_DesignId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Order_DesignId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Order_DesignId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "Order_Id",
                table: "Design",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Design_Order_Id",
                table: "Design",
                column: "Order_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Design_Orders_Order_Id",
                table: "Design",
                column: "Order_Id",
                principalTable: "Orders",
                principalColumn: "Order_Id");
        }
    }
}
