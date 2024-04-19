using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static CatGarden.Common.EntityValidationConstants.User;

namespace CatGarden.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;
    }
}
