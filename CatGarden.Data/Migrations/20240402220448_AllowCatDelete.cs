using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatGarden.Data.Migrations
{
    public partial class AllowCatDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionApplications_Cats_CatId",
                table: "AdoptionApplications");

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Cats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionApplications_Cats_CatId",
                table: "AdoptionApplications",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionApplications_Cats_CatId",
                table: "AdoptionApplications");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Cats");

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "Age", "AvailabilityStatus", "Breed", "CatteryId", "CoatLength", "Color", "DateAdded", "Description", "Gender", "Name", "UserId" },
                values: new object[] { 1, 2, 0, 41, 1, 2, 3, new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "White furball, a picture of serenity, absolutely loves lounging around.", 0, "Jimmy", null });

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionApplications_Cats_CatId",
                table: "AdoptionApplications",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
