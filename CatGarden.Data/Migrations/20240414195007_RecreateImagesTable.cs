using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatGarden.Data.Migrations
{
    public partial class RecreateImagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"), // This line enables identity for the Id column
                    Name = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    IsCover = table.Column<bool>(nullable: false),
                    CatId = table.Column<int>(nullable: true),
                    CatteryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // You may need to adjust the onDelete behavior based on your requirements
                    table.ForeignKey(
                        name: "FK_Images_Catteries_CatteryId",
                        column: x => x.CatteryId,
                        principalTable: "Catteries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // You may need to adjust the onDelete behavior based on your requirements
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_CatId",
                table: "Images",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_CatteryId",
                table: "Images",
                column: "CatteryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            // If you want to recreate the Images table in the Down method, you would need to create another migration to revert to this state
            // Recreating the table in the Down method could cause data loss if there are any records in the Images table
        }
    }
}
