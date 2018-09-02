using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtg.Card.Tracker.Data.Migrations
{
    public partial class AddNameToMagicCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cards");
        }
    }
}
