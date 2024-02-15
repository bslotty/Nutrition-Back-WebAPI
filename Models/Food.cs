using NutritionAPI.Models.Client_Requests;

namespace NutritionAPI.Models
{
    public class Food
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;


        public float ServingSize { get; set; } = 0;
        public string ServingSizeMeasurementType { get; set; } = "Grams";


        public float Protein { get; set; } = 0;
        public float Fat { get; set; } = 0;
        public float Carbs { get; set; } = 0;

        public float Sugar { get; set; } = 0;
        public float Fiber { get; set; } = 0;
        public float Sodium { get; set; } = 0;

        public Food(Guid id) {
            Id = id;
        }

        public void ApplyFromClientRequest( Food_CR payload) {
            Name = payload.name;
            Brand =  payload.brand;
            Protein = float.Parse( payload.protein );
            Fat = float.Parse( payload.fat );
            Carbs = float.Parse( payload.carbs );
            Fiber = float.Parse( payload.fiber );
            Sodium = float.Parse( payload.sodium );
            Sugar = float.Parse( payload.sugar );
            ServingSize = float.Parse( payload.servingSize ) ;
            ServingSizeMeasurementType = payload.servingSizeMeasurementType;
        } 
    }
}
