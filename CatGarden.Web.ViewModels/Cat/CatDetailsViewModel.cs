﻿
namespace CatGarden.ViewModels.Cat
{
    public class CatDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public string CoatLength { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string CatteryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; } 
        public string CoverImageUrl { get; set; } = string.Empty;

        public ICollection<string> ImageUrls { get; set; } = new List<string>();

    }
}
