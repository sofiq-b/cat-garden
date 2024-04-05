using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CatGarden.Web.Controllers
{
    [Authorize]
    public class CatteryOwnerController : Controller
    {
        private readonly ICatteryOwnerService catteryOwnerService;

        public CatteryOwnerController(ICatteryOwnerService catteryOwnerService)
        {
            this.catteryOwnerService = catteryOwnerService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isCatteryOwner = await this.catteryOwnerService.CatteryOwnerExistsByUserId(userId);

            if (isCatteryOwner)
            {
                return this.BadRequest();
            }

            return View();
        }
    }
}
