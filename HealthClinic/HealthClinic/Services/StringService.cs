using System;
using System.Globalization;
using System.Diagnostics;

namespace HealthClinic
{
    public static class StringService
    {
        public static string ToPascalCase(string input)
        {
            var resultBuilder = new System.Text.StringBuilder();
            foreach (char c in input)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    resultBuilder.Append(" ");
                }
                else
                {
                    resultBuilder.Append(c);
                }
            }

            string result = resultBuilder.ToString();
            result = result.ToLower();

            var textInfo = new CultureInfo("en-US", false).TextInfo;

            return textInfo.ToTitleCase(result).Replace(" ", String.Empty);
        }

        public static string ToMonthDayYear(DateTime input)
        {
            var formattedString = $"{input.ToString("MMM")} {input.Day}, '{input.ToString("yy")}";
            Debug.WriteLine(formattedString);

            return formattedString;
        }
    }
}
