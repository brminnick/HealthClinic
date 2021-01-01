using System;

namespace HealthClinic.Shared
{
    public record FoodLogModel(int Id, string Description, double Quantity, DateTime MealTime, string Tags, int Calories, double ProteinInGrams, double FatInGrams, double CarbohydratesInGrams, double SodiumInGrams)
    {
        public string Description_PascalCase => Description.ToPascalCase();
        public string MealTime_Formatted => MealTime.ToMonthDayYear();
        public string Protein_Formatted => $"{ProteinInGrams}g";
        public string Fat_Formatted => $"{FatInGrams}g";
        public string Carbohydrates_Formatted => $"{CarbohydratesInGrams}g";
    }
}

namespace System.Runtime.CompilerServices
{
    public class IsExternalInit { }
}