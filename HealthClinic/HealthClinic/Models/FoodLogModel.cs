using System;

namespace HealthClinic
{
    public class FoodLogModel
    {
        public string Description_PascalCase => StringService.ToPascalCase(Description);

        public int Id { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public DateTime MealTime { get; set; }
        public string Tags { get; set; }
        public int Calories { get; set; }
        public double ProteinInGrams { get; set; }
        public double FatInGrams { get; set; }
        public double CarbohydratesInGrams { get; set; }
        public double SodiumInGrams { get; set; }
    }
}
