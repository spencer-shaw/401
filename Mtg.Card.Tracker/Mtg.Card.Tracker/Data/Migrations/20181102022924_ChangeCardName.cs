using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtg.Card.Tracker.Data.Migrations
{
    public partial class ChangeCardName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_AspNetUsers_IdentityUserId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_AspNetUsers_MtgUserId",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "MagicCards");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_MtgUserId",
                table: "MagicCards",
                newName: "IX_MagicCards_MtgUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_IdentityUserId",
                table: "MagicCards",
                newName: "IX_MagicCards_IdentityUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MagicCards",
                table: "MagicCards",
                column: "MagicCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_MagicCards_AspNetUsers_IdentityUserId",
                table: "MagicCards",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MagicCards_AspNetUsers_MtgUserId",
                table: "MagicCards",
                column: "MtgUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MagicCards_AspNetUsers_IdentityUserId",
                table: "MagicCards");

            migrationBuilder.DropForeignKey(
                name: "FK_MagicCards_AspNetUsers_MtgUserId",
                table: "MagicCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MagicCards",
                table: "MagicCards");

            migrationBuilder.RenameTable(
                name: "MagicCards",
                newName: "Cards");

            migrationBuilder.RenameIndex(
                name: "IX_MagicCards_MtgUserId",
                table: "Cards",
                newName: "IX_Cards_MtgUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MagicCards_IdentityUserId",
                table: "Cards",
                newName: "IX_Cards_IdentityUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "MagicCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_AspNetUsers_IdentityUserId",
                table: "Cards",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_AspNetUsers_MtgUserId",
                table: "Cards",
                column: "MtgUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
