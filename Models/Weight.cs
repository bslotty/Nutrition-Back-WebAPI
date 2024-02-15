using NutritionAPI.Models.ClientRequests;

namespace NutritionAPI.Models
{
    public class Weight
    {
        public Guid Id { get; set; } = Guid.Empty;
        public float Pounds { get; set; }
        public DateTime Date { get; set; }


        public Weight(Guid id)
        {
           Id = id;
        }

        public void ApplyFromClientRequest( Weight_CR request ) {
            Date = DateTime.Parse( request.date );
            Pounds = float.Parse( request.pounds );
        }
    }
}
