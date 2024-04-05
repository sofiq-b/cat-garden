using System.ComponentModel.DataAnnotations;
using static CatGarden.Common.EntityValidationConstants.CatteryOwner;

namespace CatGarden.Web.ViewModels.CatteryOwner
{
    public class BecomeCatteryOwnerFormModel
    {
        [Required]
        [StringLength(PhoneNumberMaxLength,
            MinimumLength = PhoneNumberMinLength)]
        [Phone]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
    }
}
