using CatGarden.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ImageEntityConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        
        builder
            .HasOne(image => image.Cat)
            .WithMany(cat => cat.Images)
            .HasForeignKey(image => image.CatId)
            .OnDelete(DeleteBehavior.Restrict); 

       
        builder
            .HasOne(image => image.Cattery)
            .WithMany(cattery => cattery.Images)
            .HasForeignKey(image => image.CatteryId)
            .OnDelete(DeleteBehavior.Restrict); 


        builder.HasData(this.GenerateImages());
    }

    private Image[] GenerateImages()
    {
        ICollection<Image> images = new HashSet<Image>();

        Image image;

        image = new Image
        {
            Id = 1,
            CatId = 1,  
            Name = "jimmy_image2.jpg",
            URL = "/cats/jimmy_1/jimmy_image2.jpg"
        };
        images.Add(image);

        image = new Image
        {
            Id = 2,
            CatId = 1,
            Name = "jimmy_image3.jpg",
            URL = "/cats/jimmy_1/jimmy_image3.jpg"
        };
        images.Add(image);

        image = new Image
        {
            Id = 3,
            CatId = 2,
            Name = "nagi_image2.jpg",
            URL = "/cats/nagi_2/nagi_image2.jpg"
        };
        images.Add(image);

        image = new Image
        {
            Id = 4,
            CatId = 2,
            Name = "nagi_image3.jpg",
            URL = "/cats/nagi_2/nagi_image3.jpg"
        };
        images.Add(image);

        image = new Image
        {
            Id = 5,
            CatId = 1,
            Name = "jimmy_cover.jpg",
            URL = "/cats/jimmy_1/jimmy_cover.jpg",
            IsCover = true
        };
        images.Add(image);

        image = new Image
        {
            Id = 6,
            CatId = 2,
            Name = "nagi_cover.jpg",
            URL = "/cats/nagi_2/nagi_cover.jpg",
            IsCover = true
        };
        images.Add(image);

        image = new Image
        {
            Id = 7,
            CatteryId = 2,
            Name = "simone-nolgo-WMeQtoH-a3w-unsplash.jpg",
            URL = "/catteries/purrfect-paws_2/simone-nolgo-WMeQtoH-a3w-unsplash.jpg",
            IsCover = true
        };
        images.Add(image);

        image = new Image
        {
            Id = 8,
            CatteryId = 1,
            Name = "ries-bosch-sj16pUqOoco-unsplash.jpg",
            URL = "/catteries/whisker-haven_1/ries-bosch-sj16pUqOoco-unsplash.jpg",
            IsCover = true
        };
        images.Add(image);

        return images.ToArray();
    }
}