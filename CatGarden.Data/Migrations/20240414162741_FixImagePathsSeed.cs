using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatGarden.Data.Migrations
{
    public partial class FixImagePathsSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                column: "URL",
                value: "/cats/jimmy_1/jimmy_image2.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                column: "URL",
                value: "/cats/jimmy_1/jimmy_image3.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                column: "URL",
                value: "/cats/nagi_2/nagi_image2.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                column: "URL",
                value: "/cats/nagi_2/nagi_image3.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5,
                column: "URL",
                value: "/cats/jimmy_1/jimmy_cover.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 6,
                column: "URL",
                value: "/cats/nagi_2/nagi_cover.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 7,
                column: "URL",
                value: "/catteries/purrfect-paws_2/simone-nolgo-WMeQtoH-a3w-unsplash.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 8,
                column: "URL",
                value: "/catteries/whisker-haven_1/ries-bosch-sj16pUqOoco-unsplash.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                column: "URL",
                value: "/cats/gallery/jimmy_image2.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                column: "URL",
                value: "/cats/gallery/jimmy_image3.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                column: "URL",
                value: "/cats/gallery/nagi_image2.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                column: "URL",
                value: "/cats/gallery/nagi_image3.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5,
                column: "URL",
                value: "/cats/cover/jimmy_cover.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 6,
                column: "URL",
                value: "/cats/cover/nagi_cover.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 7,
                column: "URL",
                value: "/catteris/cover/simone-nolgo-WMeQtoH-a3w-unsplash.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 8,
                column: "URL",
                value: "/catteries/cover/ries-bosch-sj16pUqOoco-unsplash.jpg");
        }
    }
}
