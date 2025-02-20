using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeePeek.Data.Migrations
{
    /// <inheritdoc />
    public partial class v10010addpostsandphoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ShopPhoto_Url",
                table: "ShopPhoto",
                column: "Url");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShopPhoto_Url",
                table: "ShopPhoto");
        }
    }
}
