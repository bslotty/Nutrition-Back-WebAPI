namespace NutritionAPI.Models.Client_Requests {
    public class Recipe_CR {
        public string id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;

        public List<RecipePart_CR> contents { get; set; } = new List<RecipePart_CR>();
    }
}
