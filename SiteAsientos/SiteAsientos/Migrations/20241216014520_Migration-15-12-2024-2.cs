using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteAsientos.Migrations
{
    /// <inheritdoc />
    public partial class Migration151220242 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order_SupplierId",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order_SupplierId",
                table: "Orders",
                type: "int",
                nullable: true);
        }
    }
}
