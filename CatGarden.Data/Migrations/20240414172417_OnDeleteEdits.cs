using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatGarden.Data.Migrations
{
    public partial class OnDeleteEdits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Cats_CatId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Catteries_CatteryId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Cats_CatId",
                table: "Images",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Catteries_CatteryId",
                table: "Images",
                column: "CatteryId",
                principalTable: "Catteries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Cats_CatId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Catteries_CatteryId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Cats_CatId",
                table: "Images",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Catteries_CatteryId",
                table: "Images",
                column: "CatteryId",
                principalTable: "Catteries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
