using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Testovoe_zadaniye.Migrations
{
    public partial class AvatarChangDatType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_TouristTour_Id",
                table: "TouristTour");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Tourists",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Avatar",
                table: "Tourists",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TouristTour_Id",
                table: "TouristTour",
                column: "Id");
        }
    }
}
