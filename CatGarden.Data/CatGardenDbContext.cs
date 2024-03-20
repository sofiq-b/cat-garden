using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CatGarden.Data
{
    public class CatGardenDbContext : IdentityDbContext
    {
        public CatGardenDbContext(DbContextOptions<CatGardenDbContext> options)
            : base(options)
        {
        }
    }
}
