namespace CatGarden.Web.ViewModels.Cattery
{
    public class CatteryDisplayViewModel
    {
        public int Id { get; set; } 
        public string CoverImageUrl { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime EstablishmentDate { get; set; }
    }
}
