using CatGarden.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CatGarden.Data
{
    public class CatGardenDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public CatGardenDbContext(DbContextOptions<CatGardenDbContext> options)
            : base(options)
        {

        }

        public DbSet<Cat> Cats { get; set; } = null!;
        public DbSet<Cattery> Catteries { get; set; } = null!;
        public DbSet<AdoptionApplication> AdoptionApplications { get; set; } = null!;
        public DbSet<CatteryOwner> CatteryOwners { get; set; } = null!;
        public DbSet<UserFavCat> UsersFavCats { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(CatGardenDbContext)) ??
                Assembly.GetExecutingAssembly();

            builder.ApplyConfigurationsFromAssembly(configAssembly);

            base.OnModelCreating(builder);

            
        }
    }
}
