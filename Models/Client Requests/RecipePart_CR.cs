namespace NutritionAPI.Models.Client_Requests {
    public class RecipePart_CR {
        public string id { get; set; } = string.Empty;
        public string recipeId { get; set; } = string.Empty;
        public string foodId { get; set; } = string.Empty;

        public Food_CR food { get; set; }
        public string amountMeasurementType { get; set; } = string.Empty;
        public string amount { get; set; } = string.Empty;

    }
}
