using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatGarden.Data.Migrations
{
    public partial class ReseedImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Delete existing images
            migrationBuilder.Sql("DELETE FROM Images");

            // Add new images
            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CatId", "CatteryId", "Name", "URL", "IsCover" },
                values: new object[,]
                {
                    { 1, 1, null, "jimmy_image2.jpg", "/cats/jimmy_1/jimmy_image2.jpg", false },
                    { 2, 1, null, "jimmy_image3.jpg", "/cats/jimmy_1/jimmy_image3.jpg", false },
                    { 3, 2, null, "nagi_image2.jpg", "/cats/nagi_2/nagi_image2.jpg", false },
                    { 4, 2, null, "nagi_image3.jpg", "/cats/nagi_2/nagi_image3.jpg", false },
                    { 5, 1, null, "jimmy_cover.jpg", "/cats/jimmy_1/jimmy_cover.jpg", true },
                    { 6, 2, null, "nagi_cover.jpg", "/cats/nagi_2/nagi_cover.jpg", true },
                    { 7, null, 2, "simone-nolgo-WMeQtoH-a3w-unsplash.jpg", "/catteries/purrfect-paws_2/simone-nolgo-WMeQtoH-a3w-unsplash.jpg", true },
                    { 8, null, 1, "ries-bosch-sj16pUqOoco-unsplash.jpg", "/catteries/whisker-haven_1/ries-bosch-sj16pUqOoco-unsplash.jpg", true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No need to specify down migration as it will drop and recreate the table.
        }
    }
}
