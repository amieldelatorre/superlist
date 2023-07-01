using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.ConstrainedExecution;

namespace vlist.Validation
{
    public class DateFormatAttribute : ValidationAttribute
    {
        public const string DateFormat = "yyyy-MM-ddTHH:mm:sszz";
        public override bool IsValid(object? value)
        {
            bool result = DateTime.TryParseExact((string?)value, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

            return result;
        }
    }
}
