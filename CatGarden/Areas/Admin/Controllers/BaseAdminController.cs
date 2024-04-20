using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CatGarden.Common.GeneralApplicationConstants;

namespace CatGarden.Web.Areas.Admin.Controllers
{
    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class BaseAdminController : Controller
    {

    }
}
