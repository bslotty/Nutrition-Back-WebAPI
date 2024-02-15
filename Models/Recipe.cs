using NutritionAPI.Models.Client_Requests;

namespace NutritionAPI.Models {
    public class Recipe {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public List<RecipePart> Contents { get; set; } =  new List<RecipePart>();

        public Recipe( Guid id ) {
            Id = id;
        }

        public void ApplyFromClientRequest( Recipe_CR payload ) {
            Name = payload.name;
        }

        public Food ToFood() {
            Food f = new Food(this.Id);
            return f;
        }

    }
}
