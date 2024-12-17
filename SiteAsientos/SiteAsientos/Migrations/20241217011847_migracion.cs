using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteAsientos.Migrations
{
    /// <inheritdoc />
    public partial class migracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Design_Image_DesignId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Orders_Invoice_OrderId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Design_Order_DesignId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_Orders_Visit_OrderId",
                table: "Visit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Order_DesignId",
                table: "Order",
                newName: "IX_Order_Order_DesignId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_Image_DesignId",
                table: "Image",
                newName: "IX_Image_Image_DesignId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Order_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "Image_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Design_Image_DesignId",
                table: "Image",
                column: "Image_DesignId",
                principalTable: "Design",
                principalColumn: "Design_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Order_Invoice_OrderId",
                table: "Invoice",
                column: "Invoice_OrderId",
                principalTable: "Order",
                principalColumn: "Order_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Design_Order_DesignId",
                table: "Order",
                column: "Order_DesignId",
                principalTable: "Design",
                principalColumn: "Design_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_Order_Visit_OrderId",
                table: "Visit",
                column: "Visit_OrderId",
                principalTable: "Order",
                principalColumn: "Order_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Design_Image_DesignId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Order_Invoice_OrderId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Design_Order_DesignId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_Order_Visit_OrderId",
                table: "Visit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Order_DesignId",
                table: "Orders",
                newName: "IX_Orders_Order_DesignId");

            migrationBuilder.RenameIndex(
                name: "IX_Image_Image_DesignId",
                table: "Images",
                newName: "IX_Images_Image_DesignId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Order_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Image_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Design_Image_DesignId",
                table: "Images",
                column: "Image_DesignId",
                principalTable: "Design",
                principalColumn: "Design_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Orders_Invoice_OrderId",
                table: "Invoice",
                column: "Invoice_OrderId",
                principalTable: "Orders",
                principalColumn: "Order_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Design_Order_DesignId",
                table: "Orders",
                column: "Order_DesignId",
                principalTable: "Design",
                principalColumn: "Design_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_Orders_Visit_OrderId",
                table: "Visit",
                column: "Visit_OrderId",
                principalTable: "Orders",
                principalColumn: "Order_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
