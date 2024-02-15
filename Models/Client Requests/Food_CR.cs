namespace NutritionAPI.Models.Client_Requests {
    public class Food_CR {

        public string brand { get; set; }  = String.Empty;
        public string name { get; set; } = String.Empty;
        public string id { get; set; } = String.Empty;
        public string servingSizeMeasurementType { get; set; }  = string.Empty;
        public string servingSize { get; set; } = String.Empty;
        public string protein { get; set; } = String.Empty;
        public string fat { get; set; } = String.Empty;
        public string carbs { get; set; } = String.Empty;
        public string fiber { get; set; } = String.Empty;
        public string sugar { get; set; } = String.Empty;
        public string sodium { get; set; } = String.Empty;
    }
}
