using System.ComponentModel.DataAnnotations;
using static CatGarden.Common.EntityValidationConstants.ContactInfo;

namespace CatGarden.Data.Models
{
    public class ContactInfo
    {
        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = string.Empty;

        public IList<Cattery> Catteries { get; set; } = new List<Cattery>();
    }
}
