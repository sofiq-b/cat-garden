using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatGarden.Data.Migrations
{
    public partial class SeedImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CatId", "Name", "URL" },
                values: new object[,]
                {
                    { 1, 1, "jimmy_image2.jpg", "/cats/gallery/jimmy_image2.jpg" },
                    { 2, 1, "jimmy_image3.jpg", "/cats/gallery/jimmy_image3.jpg" },
                    { 3, 2, "nagi_image2.jpg.jpg", "/cats/gallery/nagi_image2.jpg.jpg" },
                    { 4, 2, "nagi_image3.jpg", "/cats/gallery/nagi_image3.jpg.jpg" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
