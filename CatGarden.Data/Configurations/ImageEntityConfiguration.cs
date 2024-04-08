using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CatGarden.Data.Models;

public class ImageEntityConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        
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
            URL = "/cats/gallery/jimmy_image2.jpg"
        };
        images.Add(image);

        image = new Image
        {
            Id = 2,
            CatId = 1,
            Name = "jimmy_image3.jpg",
            URL = "/cats/gallery/jimmy_image3.jpg"
        };
        images.Add(image);

        image = new Image
        {
            Id = 3,
            CatId = 2,
            Name = "nagi_image2.jpg.jpg",
            URL = "/cats/gallery/nagi_image2.jpg.jpg"
        };
        images.Add(image);

        image = new Image
        {
            Id = 4,
            CatId = 2,
            Name = "nagi_image3.jpg",
            URL = "/cats/gallery/nagi_image3.jpg.jpg"
        };
        images.Add(image);

        return images.ToArray();
    }
}