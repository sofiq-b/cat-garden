using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatGarden.Data.Models
{
    public class UserFavCat
    {
        [Required]
        public int CatId { get; set; }

        [ForeignKey(nameof(CatId))]
        public Cat Cat { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; } 

        [Required]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
    }
}
