using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatGarden.Data.Migrations
{
    public partial class AddInitialImages : Migration
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
                    { 3, 2, "nagi_image2.jpg", "/cats/gallery/nagi_image2.jpg" },
                    { 4, 2, "nagi_image3.jpg", "/cats/gallery/nagi_image3.jpg" }
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4 });
        }
    }
}
