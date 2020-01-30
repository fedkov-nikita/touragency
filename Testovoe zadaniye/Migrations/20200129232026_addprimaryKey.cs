using Microsoft.EntityFrameworkCore.Migrations;

namespace Testovoe_zadaniye.Migrations
{
    public partial class addprimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_TouristTour_Id",
                table: "TouristTour",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_TouristTour_Id",
                table: "TouristTour");
        }
    }
}
