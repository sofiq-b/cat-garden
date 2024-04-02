using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatGarden.Web.Controllers
{
    [Authorize]
    public class CatteryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
