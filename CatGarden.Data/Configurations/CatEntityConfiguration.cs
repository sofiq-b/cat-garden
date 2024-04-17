using CatGarden.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;
using static CatGarden.Common.Enums;

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

        cat = new Cat()
        {
            Id = 2,
            Name = "Nagi",
            Age = 2,
            Gender = Gender.Male,
            Breed = Breed.British_Shorthair,
            Color = Color.Gray,
            CoatLength = CoatLength.Short,
            Description = "Playful and very energetic, knows how to do a handshake!",
            DateAdded = DateTime.ParseExact("14/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            CatteryId = 1,
            AvailabilityStatus = AvailabilityStatus.Available
        };
        cats.Add(cat);

        return cats.ToArray();
    }
}
