using Microsoft.AspNetCore.Identity;

namespace CatGarden.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public IList<Cat> AdoptedCats { get; set; } = new List<Cat>();

    }
}
