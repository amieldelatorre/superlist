using System.ComponentModel.DataAnnotations;
using vlist.Validation;

namespace vlist.Models.VList
{
    public class VListCreate
    {   
        [ExpiryDateValidation]
        [Required]
        public required string Expiry { get; set; }
        [Required]
        public required string CreatedBy { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 8, ErrorMessage = "The field PassPhrase must be a string with a minimum length of 8")]
        public required string PassPhrase { get; set; }
        public string Description { get; set; } = string.Empty;
    
        public Dictionary<string, List<string>> ValidateListCreate()
        {
            Dictionary<string, List<string>> errors = new ();

            if (string.IsNullOrEmpty(CreatedBy.Trim()))
                errors[nameof(CreatedBy)] = new List<string>() { "Cannot be null or empty" };
            if (string.IsNullOrEmpty(Title.Trim()))
                errors[nameof(Title)] = new List<string>() { "Cannot be null or empty" };
            if (string.IsNullOrEmpty(PassPhrase.Trim()) || PassPhrase.Trim().Length < 8)
                errors[nameof(PassPhrase)] = new List<string>() { "Must be a string with a minimum length of 8" };
            if (!(new ExpiryDateValidationAttribute().IsValid(Expiry)))
                errors[nameof(Expiry)] = new List<string>() { new ExpiryDateValidationAttribute().FormatErrorMessage(nameof(Expiry)) };

            return errors;
        }
    }
}
