using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatGarden.Web.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
