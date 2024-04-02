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
                CatteryId = 1,
                AvailabilityStatus = AvailabilityStatus.Available
            };
            cats.Add(cat);

            

            return cats.ToArray();
        }
    }
}
