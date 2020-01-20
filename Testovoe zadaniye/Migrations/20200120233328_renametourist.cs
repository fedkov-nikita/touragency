using Microsoft.EntityFrameworkCore.Migrations;

namespace Testovoe_zadaniye.Migrations
{
    public partial class renametourist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tourist_Guides_GuideId",
                table: "Tourist");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristTour_Tourist_TouristId",
                table: "TouristTour");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tourist",
                table: "Tourist");

            migrationBuilder.RenameTable(
                name: "Tourist",
                newName: "Tourists");

            migrationBuilder.RenameIndex(
                name: "IX_Tourist_GuideId",
                table: "Tourists",
                newName: "IX_Tourists_GuideId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tourists",
                table: "Tourists",
                column: "Touristid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tourists_Guides_GuideId",
                table: "Tourists",
                column: "GuideId",
                principalTable: "Guides",
                principalColumn: "GuideId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TouristTour_Tourists_TouristId",
                table: "TouristTour",
                column: "TouristId",
                principalTable: "Tourists",
                principalColumn: "Touristid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tourists_Guides_GuideId",
                table: "Tourists");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristTour_Tourists_TouristId",
                table: "TouristTour");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tourists",
                table: "Tourists");

            migrationBuilder.RenameTable(
                name: "Tourists",
                newName: "Tourist");

            migrationBuilder.RenameIndex(
                name: "IX_Tourists_GuideId",
                table: "Tourist",
                newName: "IX_Tourist_GuideId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tourist",
                table: "Tourist",
                column: "Touristid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tourist_Guides_GuideId",
                table: "Tourist",
                column: "GuideId",
                principalTable: "Guides",
                principalColumn: "GuideId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TouristTour_Tourist_TouristId",
                table: "TouristTour",
                column: "TouristId",
                principalTable: "Tourist",
                principalColumn: "Touristid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
