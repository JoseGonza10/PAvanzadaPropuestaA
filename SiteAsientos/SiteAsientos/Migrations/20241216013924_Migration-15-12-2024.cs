using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteAsientos.Migrations
{
    /// <inheritdoc />
    public partial class Migration15122024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Employee_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Employee_MiddleName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Employee_LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Employee_Password = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Employee_Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Employee_Phone = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Employee_Type = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Employee_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Employee_Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Supplier_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Supplier_Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Supplier_Address = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Supplier_Phone = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Supplier_Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Supplier_DateAdded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Supplier_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Supplier_Id);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Material_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Material_Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Material_Status = table.Column<bool>(type: "bit", nullable: false),
                    Material_SupplierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Material_Id);
                    table.ForeignKey(
                        name: "FK_Material_Supplier_Material_SupplierId",
                        column: x => x.Material_SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Supplier_Id");
                });

            migrationBuilder.CreateTable(
                name: "Design",
                columns: table => new
                {
                    Design_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Design_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Design_MaterialId = table.Column<int>(type: "int", nullable: true),
                    Design_Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Design_Status = table.Column<bool>(type: "bit", nullable: false),
                    Design_Price = table.Column<float>(type: "real", nullable: false),
                    Design_Taxable = table.Column<float>(type: "real", nullable: false),
                    Design_Service = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Design", x => x.Design_Id);
                    table.ForeignKey(
                        name: "FK_Design_Material_Design_MaterialId",
                        column: x => x.Design_MaterialId,
                        principalTable: "Material",
                        principalColumn: "Material_Id");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Image_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image_Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Image_ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Image_Id);
                    table.ForeignKey(
                        name: "FK_Images_Design_Image_ImageId",
                        column: x => x.Image_ImageId,
                        principalTable: "Design",
                        principalColumn: "Design_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Order_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Order_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Order_CustomerName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Order_CustomerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Order_CustomerPhone = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Order_CustomerEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Order_CarBrand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_CarModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_ModelYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_CustomerPlateNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Order_DesignId = table.Column<int>(type: "int", nullable: true),
                    Order_Status = table.Column<bool>(type: "bit", nullable: false),
                    Order_Embroidery = table.Column<bool>(type: "bit", nullable: false),
                    Order_SupplierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Order_Id);
                    table.ForeignKey(
                        name: "FK_Orders_Design_Order_DesignId",
                        column: x => x.Order_DesignId,
                        principalTable: "Design",
                        principalColumn: "Design_Id");
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Invoice_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Invoice_Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Invoice_OrderId = table.Column<int>(type: "int", nullable: false),
                    Invoice_Total = table.Column<float>(type: "real", nullable: true),
                    Invoice_AmoundPaid = table.Column<float>(type: "real", nullable: true),
                    Invoice_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Invoice_CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Invoice_CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Invoice_CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Invoice_Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Orders_Invoice_OrderId",
                        column: x => x.Invoice_OrderId,
                        principalTable: "Orders",
                        principalColumn: "Order_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visit",
                columns: table => new
                {
                    Visit_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Visit_Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Visit_OrderId = table.Column<int>(type: "int", nullable: false),
                    Visit_EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Visit_Type = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Visit_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Visit_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visit", x => x.Visit_Id);
                    table.ForeignKey(
                        name: "FK_Visit_Employee_Visit_EmployeeId",
                        column: x => x.Visit_EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Employee_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visit_Orders_Visit_OrderId",
                        column: x => x.Visit_OrderId,
                        principalTable: "Orders",
                        principalColumn: "Order_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Design_Design_MaterialId",
                table: "Design",
                column: "Design_MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Image_ImageId",
                table: "Images",
                column: "Image_ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Invoice_OrderId",
                table: "Invoice",
                column: "Invoice_OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Material_Material_SupplierId",
                table: "Material",
                column: "Material_SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Order_DesignId",
                table: "Orders",
                column: "Order_DesignId");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_Visit_EmployeeId",
                table: "Visit",
                column: "Visit_EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_Visit_OrderId",
                table: "Visit",
                column: "Visit_OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Visit");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Design");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
