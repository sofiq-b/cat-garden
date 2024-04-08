using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatGarden.Data.Migrations
{
    public partial class SeedCatsForReal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "Age", "AvailabilityStatus", "Breed", "CatteryId", "CoatLength", "Color", "CoverImageUrl", "DateAdded", "Description", "Gender", "Name", "UserId" },
                values: new object[] { 1, 2, 0, 41, 1, 2, 3, "/cats/cover/jimmy_cover.jpg", new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "White furball, a picture of serenity, absolutely loves lounging around.", 0, "Jimmy", null });

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "Age", "AvailabilityStatus", "Breed", "CatteryId", "CoatLength", "Color", "CoverImageUrl", "DateAdded", "Description", "Gender", "Name", "UserId" },
                values: new object[] { 2, 2, 0, 10, 1, 1, 6, "/cats/cover/nagi_cover.jpg", new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Playful and very energetic, knows how to do a handshake!", 0, "Nagi", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
