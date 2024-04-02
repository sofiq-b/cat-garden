using Microsoft.AspNetCore.Mvc;

namespace CatGarden.Web.Controllers
{
    public class CatteryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
