using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeePeek.Data.Migrations
{
    /// <inheritdoc />
    public partial class v1003CPG21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewShopId",
                table: "ShopPhoto",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewShopId",
                table: "Schedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewShopId",
                table: "ScheduleExceptions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReviewShops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NotValidatedAddress = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AddressId = table.Column<int>(type: "integer", nullable: true),
                    ShopContactId = table.Column<int>(type: "integer", nullable: true),
                    ShopId = table.Column<int>(type: "integer", nullable: true),
                    ReviewStatus = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ShopContactsId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewShops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewShops_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReviewShops_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewShops_ShopContacts_ShopContactsId",
                        column: x => x.ShopContactsId,
                        principalTable: "ShopContacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReviewShops_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopPhoto_ReviewShopId",
                table: "ShopPhoto",
                column: "ReviewShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ReviewShopId",
                table: "Schedules",
                column: "ReviewShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleExceptions_ReviewShopId",
                table: "ScheduleExceptions",
                column: "ReviewShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewShops_AddressId",
                table: "ReviewShops",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewShops_ShopContactsId",
                table: "ReviewShops",
                column: "ShopContactsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewShops_ShopId",
                table: "ReviewShops",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewShops_UserId",
                table: "ReviewShops",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleExceptions_ReviewShops_ReviewShopId",
                table: "ScheduleExceptions",
                column: "ReviewShopId",
                principalTable: "ReviewShops",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ReviewShops_ReviewShopId",
                table: "Schedules",
                column: "ReviewShopId",
                principalTable: "ReviewShops",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPhoto_ReviewShops_ReviewShopId",
                table: "ShopPhoto",
                column: "ReviewShopId",
                principalTable: "ReviewShops",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleExceptions_ReviewShops_ReviewShopId",
                table: "ScheduleExceptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ReviewShops_ReviewShopId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopPhoto_ReviewShops_ReviewShopId",
                table: "ShopPhoto");

            migrationBuilder.DropTable(
                name: "ReviewShops");

            migrationBuilder.DropIndex(
                name: "IX_ShopPhoto_ReviewShopId",
                table: "ShopPhoto");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ReviewShopId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleExceptions_ReviewShopId",
                table: "ScheduleExceptions");

            migrationBuilder.DropColumn(
                name: "ReviewShopId",
                table: "ShopPhoto");

            migrationBuilder.DropColumn(
                name: "ReviewShopId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ReviewShopId",
                table: "ScheduleExceptions");
        }
    }
}
