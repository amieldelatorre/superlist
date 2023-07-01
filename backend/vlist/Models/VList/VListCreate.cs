using System.ComponentModel.DataAnnotations;
using vlist.Validation;

namespace vlist.Models.VList
{
    public class VListCreate
    {
        [Required]
        [DateFormatAttribute(ErrorMessage = $"Accepted format is {Validation.DateFormatAttribute.DateFormat} -> 2023-07-27T00:00:00+02")]
        public required string Expiry { get; set; }
        [Required]
        public required string CreatedBy { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Description { get; set; }
    }
}
