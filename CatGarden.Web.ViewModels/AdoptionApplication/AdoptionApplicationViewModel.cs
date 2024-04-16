namespace CatGarden.Web.ViewModels.AdoptionApplication
{
    public class AdoptionApplicationViewModel
    {
        public string Username { get; set; } = string.Empty;

        public string CatName { get; set; } = string.Empty;

        public DateTime ApplicationDate { get; set; }

        public string ApplicationStatus { get; set; } = string.Empty;
    }
}
