namespace NutritionAPI.Models.Client_Requests {
    public class Meal_CR {

        public string id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;

        public string date { get; set; } = string.Empty;

        public List<MealPart_CR> parts { get; set; } = new List<MealPart_CR>();


    }
}
