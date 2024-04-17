using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatGarden.Data.Migrations
{
    public partial class OnDeleteRestrictForAdptApplAndReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionApplications_Cats_CatId",
                table: "AdoptionApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Catteries_CatteryId",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionApplications_Cats_CatId",
                table: "AdoptionApplications",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Catteries_CatteryId",
                table: "Reviews",
                column: "CatteryId",
                principalTable: "Catteries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionApplications_Cats_CatId",
                table: "AdoptionApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Catteries_CatteryId",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionApplications_Cats_CatId",
                table: "AdoptionApplications",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Catteries_CatteryId",
                table: "Reviews",
                column: "CatteryId",
                principalTable: "Catteries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
