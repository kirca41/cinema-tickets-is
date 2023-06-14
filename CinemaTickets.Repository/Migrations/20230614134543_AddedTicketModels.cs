using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTickets.Repository.Migrations
{
    public partial class AddedTicketModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketInShoppingCart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MovieScreeningId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoppingCartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketInShoppingCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketInShoppingCart_MovieScreenings_MovieScreeningId",
                        column: x => x.MovieScreeningId,
                        principalTable: "MovieScreenings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketInShoppingCart_ShoppingCart_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketInShoppingCart_MovieScreeningId",
                table: "TicketInShoppingCart",
                column: "MovieScreeningId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketInShoppingCart_ShoppingCartId",
                table: "TicketInShoppingCart",
                column: "ShoppingCartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketInShoppingCart");
        }
    }
}
