using NutritionAPI.Models.Client_Requests;

namespace NutritionAPI.Models
{
    public class MealPart
    {
        public Guid Id { get; set; }
        public Guid MealId { get; set; }
        public Food Food { get; set; }


        public float Amount { get; set; }
        public string AmountMeasurementType { get; set; } = "Grams";


        public MealPart(Guid id, Guid mealId) {
            Id = id;
            MealId = mealId;
        }

        public void ApplyFromClientRequest(MealPart_CR payload) {

            Amount = float.Parse(payload.amount);
            AmountMeasurementType = payload.amountMeasurementType;
        }

        public void ApplyClientRequestFood(Food food) {
            this.Food = food;
        }
    }
}
