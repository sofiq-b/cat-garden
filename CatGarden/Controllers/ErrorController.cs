using Microsoft.AspNetCore.Mvc;

namespace CatGarden.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("/Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found.";
                    return View("~/Views/Error/NotFound.cshtml"); // Specify the full path to the view
                case 500:
                    ViewBag.ErrorMessage = "Sorry, something went wrong on the server.";
                    return View("~/Views/Error/InternalServerError.cshtml"); // Specify the full path to the view
                default:
                    return View("~/Views/Error/NotFound.cshtml"); // Specify the full path to the view
            }
        }
    }
}
