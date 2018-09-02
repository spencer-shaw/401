using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtg.Card.Tracker.Data.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "Cards",
                newName: "MagicCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MagicCardId",
                table: "Cards",
                newName: "CardId");
        }
    }
}
