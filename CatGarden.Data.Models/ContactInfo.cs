using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatGarden.Data.Models
{
    public class ContactInfo
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public IList<Cattery> Catteries { get; set; } = new List<Cattery>();
    }
}
