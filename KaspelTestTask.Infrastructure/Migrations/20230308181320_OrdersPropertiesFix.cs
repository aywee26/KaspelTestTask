using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaspelTestTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrdersPropertiesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderTime",
                table: "Orders",
                newName: "OrderDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "OrderTime");
        }
    }
}
