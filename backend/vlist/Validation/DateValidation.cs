using System.Globalization;

namespace vlist.Validation
{
    public class DateValidation
    {
        public static readonly string[] DateFormats =  {
            "yyyy-MM-dd\\THH:mm:sszzz",
            "yyyy-MM-dd\\THH:mm:sszz",
            "yyyy-MM-dd\\THH:mm:ss\\Z" 
        };
        public static bool IsValidDate(string date)
        {
            bool result = DateTime.TryParseExact(date.Trim(), DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

            return result;
        }

        public static string InvalidDateErrorMessage(string name)
        {
            return $"Accepted date formats for {name} are {string.Join(", ", DateFormats)}";
        }

        public static DateTime ParseDate(string date)
        {
            return DateTime.ParseExact(date.Trim(), DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

    }
}
