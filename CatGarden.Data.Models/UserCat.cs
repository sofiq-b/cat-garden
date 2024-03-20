using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatGarden.Data.Models
{
    public class UserCat
    {
        [Required]
        public int CatId { get; set; }

        [ForeignKey(nameof(CatId))]
        public Cat Cat { get; set; } = null!;

        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(ApplicationUserId))]
        public IdentityUser ApplicationUser { get; set; } = null!;
    }
}
