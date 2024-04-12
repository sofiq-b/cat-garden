using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatGarden.Data.Migrations
{
    public partial class TruncateImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE Images"); // Truncate the Images table

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Cats_CatId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "Cats");

            migrationBuilder.AlterColumn<int>(
                name: "CatId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CatteryId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isCover",
                table: "Images",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "nagi_image2.jpg");

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CatId", "CatteryId", "Name", "URL", "isCover" },
                values: new object[,]
                {
                    { 5, 1, null, "jimmy_cover.jpg", "/cats/cover/jimmy_cover.jpg", true },
                    { 6, 2, null, "nagi_cover.jpg", "/cats/cover/nagi_cover.jpg", true },
                    { 7, null, 2, "simone-nolgo-WMeQtoH-a3w-unsplash.jpg", "/catteris/cover/simone-nolgo-WMeQtoH-a3w-unsplash.jpg", true },
                    { 8, null, 1, "ries-bosch-sj16pUqOoco-unsplash.jpg", "/catteries/cover/ries-bosch-sj16pUqOoco-unsplash.jpg", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_CatteryId",
                table: "Images",
                column: "CatteryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Cats_CatId",
                table: "Images",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Catteries_CatteryId",
                table: "Images",
                column: "CatteryId",
                principalTable: "Catteries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Cats_CatId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Catteries_CatteryId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_CatteryId",
                table: "Images");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "CatteryId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "isCover",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "CatId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "Cats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 1,
                column: "CoverImageUrl",
                value: "/cats/cover/jimmy_cover.jpg");

            migrationBuilder.UpdateData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 2,
                column: "CoverImageUrl",
                value: "/cats/cover/nagi_cover.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "nagi_image2.jpg.jpg");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Cats_CatId",
                table: "Images",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
