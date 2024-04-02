using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatGarden.Web.Controllers
{
    [Authorize]
    public class CatteryOwnerController : Controller
    {
        public async Task<IActionResult> Become()
        {
            return View();
        }
    }
}
