using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutritionAPI.Migrations
{
    public partial class RecipeParts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipePart_Foods_FoodId",
                table: "RecipePart");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipePart_Recipes_RecipeId",
                table: "RecipePart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipePart",
                table: "RecipePart");

            migrationBuilder.RenameTable(
                name: "RecipePart",
                newName: "RecipeParts");

            migrationBuilder.RenameIndex(
                name: "IX_RecipePart_RecipeId",
                table: "RecipeParts",
                newName: "IX_RecipeParts_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipePart_FoodId",
                table: "RecipeParts",
                newName: "IX_RecipeParts_FoodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeParts",
                table: "RecipeParts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeParts_Foods_FoodId",
                table: "RecipeParts",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeParts_Recipes_RecipeId",
                table: "RecipeParts",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeParts_Foods_FoodId",
                table: "RecipeParts");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeParts_Recipes_RecipeId",
                table: "RecipeParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeParts",
                table: "RecipeParts");

            migrationBuilder.RenameTable(
                name: "RecipeParts",
                newName: "RecipePart");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeParts_RecipeId",
                table: "RecipePart",
                newName: "IX_RecipePart_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeParts_FoodId",
                table: "RecipePart",
                newName: "IX_RecipePart_FoodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipePart",
                table: "RecipePart",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipePart_Foods_FoodId",
                table: "RecipePart",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipePart_Recipes_RecipeId",
                table: "RecipePart",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
