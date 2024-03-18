using System.ComponentModel.DataAnnotations;
using static CatGarden.Common.EntityValidationConstants.Article;

namespace CatGarden.Data.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = string.Empty;

        [Required]
        public DateTime DatePublished { get; set; } 
    }
}
