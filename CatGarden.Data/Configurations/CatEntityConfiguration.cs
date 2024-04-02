using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using CatGarden.Data.Models;
using static CatGarden.Common.Enums;

namespace CatGarden.Data.Configurations
{
    public class CatEntityConfiguration : IEntityTypeConfiguration<Cat>
    {
        public void Configure(EntityTypeBuilder<Cat> builder)
        {
            builder.HasData(this.GenerateCats());
        }

        private Cat[] GenerateCats()
        {
            ICollection<Cat> cats = new HashSet<Cat>();

            Cat cat;
            cat = new Cat()
            {
                Id = 1,
                Name = "Jimmy",
                Age = 2,
                Gender = Gender.Male,
                Breed = Breed.Scottish_Fold,
                Color = Color.White,
                CoatLength = CoatLength.Medium,
                Description = "White furball, a picture of serenity, absolutely loves lounging around.",
                DateAdded = DateTime.ParseExact("16/03/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                ImageUrl = "https://64.media.tumblr.com/0bb8d8042dcacb6c53387a367ad24d3f/3e6ef086694c0856-a9/s540x810/86572e34b50123496219e1eb70b2baab4cb4378e.jpg",
                CatteryId = 1,
                AvailabilityStatus = AvailabilityStatus.Available
            };
            cats.Add(cat);

            cat = new Cat()
            {
                Id = 2,
                Name = "Nagi",
                Age = 2,
                Gender = Gender.Male,
                Breed = Breed.British_Shorthair,
                Color = Color.Gray,
                CoatLength = CoatLength.Short,
                Description = "White furball, a picture of serenity, absolutely loves lounging around.",
                DateAdded = DateTime.ParseExact("14/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                ImageUrl = "https://64.media.tumblr.com/293f19a06c23f855e1b5148bb523ff4e/99ddb4905642cf14-8c/s2048x3072/5cb1e037fb8a57fd82178d4ffe9d3f51f3c1fe6b.jpg",
                CatteryId = 1,
                AvailabilityStatus = AvailabilityStatus.Available
            };
            cats.Add(cat);



            return cats.ToArray();
        }
    }
}
