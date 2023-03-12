using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaspelTestTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesRefactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderedBook",
                table: "OrderedBook");

            migrationBuilder.DropIndex(
                name: "IX_OrderedBook_OrderId",
                table: "OrderedBook");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderedBook",
                table: "OrderedBook",
                columns: new[] { "OrderId", "BookId" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderDate",
                table: "Orders",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedBook_BookId",
                table: "OrderedBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderDate",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderedBook",
                table: "OrderedBook");

            migrationBuilder.DropIndex(
                name: "IX_OrderedBook_BookId",
                table: "OrderedBook");

            migrationBuilder.DropIndex(
                name: "IX_Books_Title",
                table: "Books");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderedBook",
                table: "OrderedBook",
                columns: new[] { "BookId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderedBook_OrderId",
                table: "OrderedBook",
                column: "OrderId");
        }
    }
}
