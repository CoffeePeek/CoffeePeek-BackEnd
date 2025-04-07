using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeePeek.Data.Migrations
{
    /// <inheritdoc />
    public partial class v1004CPG17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ShopPhoto",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShopPhoto");
        }
    }
}
