using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DietAndFitness.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalFoodItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDirty = table.Column<bool>(nullable: false),
                    GUID = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Calories = table.Column<double>(nullable: false),
                    Carbohydrates = table.Column<double>(nullable: true),
                    Proteins = table.Column<double>(nullable: true),
                    Fats = table.Column<double>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    CookingMode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalFoodItems", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalFoodItems_GUID",
                table: "LocalFoodItems",
                column: "GUID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalFoodItems");
        }
    }
}
