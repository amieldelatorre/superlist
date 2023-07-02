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
    }
}
