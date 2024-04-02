using Microsoft.AspNetCore.Mvc;

namespace CatGarden.Web.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
