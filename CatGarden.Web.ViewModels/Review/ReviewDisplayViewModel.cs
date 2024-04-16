namespace CatGarden.Web.ViewModels.Review
{
    public class ReviewDisplayViewModel
    {
        public string Username { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
