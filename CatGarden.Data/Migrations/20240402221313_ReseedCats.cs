using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatGarden.Data.Migrations
{
    public partial class ReseedCats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "Age", "AvailabilityStatus", "Breed", "CatteryId", "CoatLength", "Color", "DateAdded", "Description", "Gender", "ImageUrl", "Name", "UserId" },
                values: new object[] { 1, 2, 0, 41, 1, 2, 3, new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "White furball, a picture of serenity, absolutely loves lounging around.", 0, "https://64.media.tumblr.com/0bb8d8042dcacb6c53387a367ad24d3f/3e6ef086694c0856-a9/s540x810/86572e34b50123496219e1eb70b2baab4cb4378e.jpg", "Jimmy", null });

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "Age", "AvailabilityStatus", "Breed", "CatteryId", "CoatLength", "Color", "DateAdded", "Description", "Gender", "ImageUrl", "Name", "UserId" },
                values: new object[] { 2, 2, 0, 10, 1, 1, 6, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "White furball, a picture of serenity, absolutely loves lounging around.", 0, "https://64.media.tumblr.com/293f19a06c23f855e1b5148bb523ff4e/99ddb4905642cf14-8c/s2048x3072/5cb1e037fb8a57fd82178d4ffe9d3f51f3c1fe6b.jpg", "Nagi", null });
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
