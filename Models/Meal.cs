using NutritionAPI.Models.Client_Requests;
using System.Text.Json.Serialization;

namespace NutritionAPI.Models
{
    public class Meal
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public DateTime Date { get; set; }

        public List<MealPart> Parts { get; set; } = new List<MealPart>();

        public Meal(Guid id) {
            Id = id;
        }

        public void ApplyFromClientRequest( Meal_CR payload ) {
            Name = payload.name;
            Date = DateTime.Parse( payload.date );
        }
    }
}
