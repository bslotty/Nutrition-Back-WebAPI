using NutritionAPI.Models.Client_Requests;

namespace NutritionAPI.Models {
    public class RecipePart {
        public Guid Id { get; set; }
        public Guid RecipeId { get; set; }
        public Food Food { get; set; }

        public float Amount { get; set; }
        public string AmountMeasurementType { get; set; } = "Grams";


        public RecipePart( Guid id, Guid recipeId ) {
            Id = id;
            RecipeId = recipeId;
        }

        public void ApplyFromClientRequest( RecipePart_CR payload ) {

            Amount = float.Parse( payload.amount );
            AmountMeasurementType = payload.amountMeasurementType;
        }

        public void ApplyClientRequestFood( Food food ) {
            this.Food = food;
        }

    }
}
