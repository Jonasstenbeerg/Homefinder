using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomefinderAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Images_Canged_Relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_PropertyObjectId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PropertyObjectId",
                table: "Images",
                column: "PropertyObjectId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_PropertyObjectId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PropertyObjectId",
                table: "Images",
                column: "PropertyObjectId");
        }
    }
}
