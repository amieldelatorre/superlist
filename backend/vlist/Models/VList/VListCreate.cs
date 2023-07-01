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
        public required string Description { get; set; }
    }
}
