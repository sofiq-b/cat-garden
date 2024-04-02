using Microsoft.AspNetCore.Mvc;

namespace CatGarden.Web.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
