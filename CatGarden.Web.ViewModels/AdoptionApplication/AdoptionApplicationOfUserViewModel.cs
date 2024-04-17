namespace CatGarden.Web.ViewModels.AdoptionApplication
{
    public class AdoptionApplicationOfUserViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        public int CatId { get; set; }
        public string CatName { get; set; } = string.Empty;

        public int CatteryId { get; set; }
        public string CatteryName { get; set; } = string.Empty;

        public DateTime ApplicationDate { get; set; }

        public string ApplicationStatus { get; set; } = string.Empty;

        public string CoverImageUrl { get; set; } = string.Empty;
    }
}
