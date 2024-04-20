using Microsoft.AspNetCore.Mvc;

namespace CatGarden.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
