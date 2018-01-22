using System;

namespace HealthClinic.Shared
{
    public class FoodLogModel
    {
        public string Description_PascalCase => Description.ToPascalCase();
        public string MealTime_Formatted => MealTime.ToMonthDayYear();
        public string Protein_Formatted => $"{ProteinInGrams}g";
        public string Fat_Formatted => $"{FatInGrams}g";
        public string Carbohydrates_Formatted => $"{CarbohydratesInGrams}g";

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
