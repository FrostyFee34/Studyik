using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class cascadedeleteonmaterials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Materials_MaterialId",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Materials_MaterialId",
                table: "Notes",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Materials_MaterialId",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Materials_MaterialId",
                table: "Notes",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
