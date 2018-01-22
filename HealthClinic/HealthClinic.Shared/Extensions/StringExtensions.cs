using System;
using System.Globalization;

namespace HealthClinic.Shared
{
    public static class StringExtensions
    {
        public static string ToPascalCase(this string input)
        {
            var resultBuilder = new System.Text.StringBuilder();
            foreach (char c in input)
            {
                if (!Char.IsLetterOrDigit(c))
                    resultBuilder.Append(" ");
                else
                    resultBuilder.Append(c);
            }

            string result = resultBuilder.ToString();
            result = result.ToLower();

            var textInfo = new CultureInfo("en-US", false).TextInfo;

            return textInfo.ToTitleCase(result).Replace(" ", String.Empty);
        }

        public static string ToMonthDayYear(this DateTime input) => 
            $"{input.ToString("MMM")} {input.Day}, '{input.ToString("yy")}";
    }
}
