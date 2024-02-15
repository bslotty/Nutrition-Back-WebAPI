using NutritionAPI.Models.Client_Requests;

namespace NutritionAPI.Models {
    public class Exercise {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Activity { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public float Weight { get; set; }
        public float Sets { get; set; }
        public float Reps { get; set; }
        public string Feedback { get; set; } = String.Empty;

        public Exercise( Guid id ) {
           Id = id;
        }

        public void ApplyFromClientRequest( Exercise_CR request ) {
            Date = DateTime.Parse( request.date );
            Activity = request.activity;
            Name = request.name;
            Weight = float.Parse(request.weight);
            Sets = float.Parse( request.sets );
            Reps = float.Parse( request.reps );
            Feedback = request.feedback ;
        }
    }
}
