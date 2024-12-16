using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteAsientos.Migrations
{
    /// <inheritdoc />
    public partial class Migration151220243 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Design_Image_ImageId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Design_Order_DesignId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Order_DesignId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Order_DesignId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Image_ImageId",
                table: "Images",
                newName: "Image_DesignId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_Image_ImageId",
                table: "Images",
                newName: "IX_Images_Image_DesignId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Design_Image_DesignId",
                table: "Images",
                column: "Image_DesignId",
                principalTable: "Design",
                principalColumn: "Design_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Design_Orders_Order_Id",
                table: "Design");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Design_Image_DesignId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Design_Order_Id",
                table: "Design");

            migrationBuilder.DropColumn(
                name: "Order_Id",
                table: "Design");

            migrationBuilder.RenameColumn(
                name: "Image_DesignId",
                table: "Images",
                newName: "Image_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_Image_DesignId",
                table: "Images",
                newName: "IX_Images_Image_ImageId");

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
                name: "FK_Images_Design_Image_ImageId",
                table: "Images",
                column: "Image_ImageId",
                principalTable: "Design",
                principalColumn: "Design_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Design_Order_DesignId",
                table: "Orders",
                column: "Order_DesignId",
                principalTable: "Design",
                principalColumn: "Design_Id");
        }
    }
}
