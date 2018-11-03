using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtg.Card.Tracker.Data.Migrations
{
    public partial class TradeOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradeOffers",
                columns: table => new
                {
                    TradeOfferId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdentityUserId = table.Column<string>(nullable: true),
                    CardOfferId = table.Column<int>(nullable: true),
                    CardRequestId = table.Column<int>(nullable: true),
                    MtgUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeOffers", x => x.TradeOfferId);
                    table.ForeignKey(
                        name: "FK_TradeOffers_MagicCards_CardOfferId",
                        column: x => x.CardOfferId,
                        principalTable: "MagicCards",
                        principalColumn: "MagicCardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeOffers_MagicCards_CardRequestId",
                        column: x => x.CardRequestId,
                        principalTable: "MagicCards",
                        principalColumn: "MagicCardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeOffers_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeOffers_AspNetUsers_MtgUserId",
                        column: x => x.MtgUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradeOffers_CardOfferId",
                table: "TradeOffers",
                column: "CardOfferId",
                unique: true,
                filter: "[CardOfferId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TradeOffers_CardRequestId",
                table: "TradeOffers",
                column: "CardRequestId",
                unique: true,
                filter: "[CardRequestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TradeOffers_IdentityUserId",
                table: "TradeOffers",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeOffers_MtgUserId",
                table: "TradeOffers",
                column: "MtgUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeOffers");
        }
    }
}
