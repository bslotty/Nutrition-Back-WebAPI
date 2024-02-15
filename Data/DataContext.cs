using NutritionAPI.Models;

namespace NutritionAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }


        public DbSet<Food> Foods => Set<Food>();
        public DbSet<Meal> Meals => Set<Meal>();
        public DbSet<MealPart> MealParts => Set<MealPart>();
        public DbSet<Weight> Weights => Set<Weight>();
        public DbSet<Recipe> Recipes => Set<Recipe>();
        public DbSet<RecipePart> RecipeParts => Set<RecipePart>();
        public DbSet<Exercise> Exercises => Set<Exercise>();

    }
}
