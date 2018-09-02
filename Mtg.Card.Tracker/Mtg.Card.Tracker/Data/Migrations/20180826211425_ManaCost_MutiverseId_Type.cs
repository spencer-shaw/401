using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtg.Card.Tracker.Data.Migrations
{
    public partial class ManaCost_MutiverseId_Type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManaCost",
                table: "Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MultiverseId",
                table: "Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Cards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManaCost",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "MultiverseId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Cards");
        }
    }
}
