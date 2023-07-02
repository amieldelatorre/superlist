using System.Globalization;

namespace vlist.Validation
{
    public class DateValidation
    {
        public const string DateFormat = "yyyy-MM-ddTHH:mm:sszz";
        public static bool IsValidDate(string date)
        {
            bool result = DateTime.TryParseExact(date.Trim(), DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

            return result;
        }

        public static string InvalidDateErrorMessage(string name)
        {
            return $"Accepted date format for {name} is {DateFormat} -> 2023-07-27T00:00:00+02";
        }

        public static DateTime ParseDate(string date)
        {
            return DateTime.ParseExact(date.Trim(), DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

    }
}
