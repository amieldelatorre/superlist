using System.ComponentModel.DataAnnotations;

namespace vlist.Validation
{
    public class ExpiryDateValidationAttribute : ValidationAttribute
    {
        public TimeSpan MinimumDuration = new(0, 1, 0, 0);
        public TimeSpan MaximumDuration = new(30, 0, 0, 0);

        public override bool IsValid(object? value)
        {
            if (value == null)
                return false;
            
            if (!DateValidation.IsValidDate((string)value))
                return false;

            DateTime now = DateTime.Now;
            DateTime minimum = now.Add(MinimumDuration);
            DateTime maximum = now.Add(MaximumDuration);

            DateTime expiryDate = DateValidation.ParseDate((string)value);

            return expiryDate >= minimum && expiryDate <= maximum;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{DateValidation.InvalidDateErrorMessage(name)}. The {name} must be less than or equal to {MaximumDuration.Days} days and greater than or equal to {MinimumDuration.Hours} hours.";
        }
    }
}
