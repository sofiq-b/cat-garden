using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatGarden.Data.Migrations
{
    public partial class ReseedCats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update the foreign key constraint to cascade deletes
            migrationBuilder.DropForeignKey(
                name: "FK_UsersFavCats_Cats_CatId",
                table: "UsersFavCats");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersFavCats_Cats_CatId",
                table: "UsersFavCats",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade); // Changed to CascadeType.Cascade

            // Delete images referencing cats
            migrationBuilder.Sql("DELETE FROM Images WHERE CatId IS NOT NULL");

            // Delete references to cats in UsersFavCats table
            migrationBuilder.Sql("DELETE FROM UsersFavCats");

            // Truncate the Cat table
            migrationBuilder.Sql("DELETE FROM Cats");

            // Seed the Cat table again
            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "Name", "Age", "Gender", "Breed", "Color", "CoatLength", "Description", "DateAdded", "CatteryId", "AvailabilityStatus" },
                values: new object[,]
                {
                    { 1, "Jimmy", 2, 1, 3, 2, 1, "White furball, a picture of serenity, absolutely loves lounging around.", new DateTime(2024, 03, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, "Nagi", 2, 1, 4, 3, 0, "Playful and very energetic, knows how to do a handshake!", new DateTime(2024, 02, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert the changes made in the Up method for Cats table
            migrationBuilder.Sql("DELETE FROM Cats");

            // Revert the changes made in the Up method for Images table
            migrationBuilder.Sql("DELETE FROM Images");

            // Revert the changes made in the Up method for UsersFavCats table
            migrationBuilder.Sql("DELETE FROM UsersFavCats");
        }
    }
}
