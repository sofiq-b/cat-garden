using Microsoft.AspNetCore.Identity;

namespace CatGarden.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }

        public IList<Cat> AdoptedCats { get; set; } = new List<Cat>();

    }
}
