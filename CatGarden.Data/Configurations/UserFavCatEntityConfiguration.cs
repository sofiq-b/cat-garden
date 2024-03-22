using CatGarden.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CatGarden.Data.Configurations
{
    public class UserFavCatEntityConfiguration : IEntityTypeConfiguration<UserFavCat>
    {
        public void Configure(EntityTypeBuilder<UserFavCat> builder)
        {
            builder
                .HasOne(e => e.Cat)
                .WithMany(e => e.UserFavCats)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasKey(sp => new
               {
                   sp.UserId,
                   sp.CatId
               });
        }
    }
}
