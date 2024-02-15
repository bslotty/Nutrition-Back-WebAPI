using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutritionAPI.Migrations
{
    public partial class ExerciseActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Activity",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activity",
                table: "Exercises");
        }
    }
}
