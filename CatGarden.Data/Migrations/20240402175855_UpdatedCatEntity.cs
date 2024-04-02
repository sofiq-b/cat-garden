using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatGarden.Data.Migrations
{
    public partial class UpdatedCatEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoatColor",
                table: "Cats");

            migrationBuilder.AddColumn<int>(
                name: "City",
                table: "Catteries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoatLength",
                table: "Cats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "Cats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Catteries",
                columns: new[] { "Id", "Address", "City", "EstablishmentDate", "Name", "OwnerId" },
                values: new object[] { 1, "5, Inzh. Georgi Belov", 19, new DateTime(2012, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Whisker Haven", new Guid("b1bfe4d3-a412-4ffe-b066-fc04238e432b") });

            migrationBuilder.InsertData(
                table: "Catteries",
                columns: new[] { "Id", "Address", "City", "EstablishmentDate", "Name", "OwnerId" },
                values: new object[] { 2, "29, Sevastokrator Kaloyan", 23, new DateTime(2006, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purrfect Paws", new Guid("b1bfe4d3-a412-4ffe-b066-fc04238e432b") });

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "Age", "AvailabilityStatus", "Breed", "CatteryId", "CoatLength", "Color", "DateAdded", "Description", "Gender", "Name", "UserId" },
                values: new object[] { 1, 2, 0, 41, 1, 2, 3, new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "White furball, a picture of serenity, absolutely loves lounging around.", 0, "Jimmy", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Catteries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Catteries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "City",
                table: "Catteries");

            migrationBuilder.DropColumn(
                name: "CoatLength",
                table: "Cats");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Cats");

            migrationBuilder.AddColumn<string>(
                name: "CoatColor",
                table: "Cats",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
