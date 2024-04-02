using Microsoft.AspNetCore.Mvc;

namespace CatGarden.Web.Controllers
{
    public class AdoptionApplicationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
