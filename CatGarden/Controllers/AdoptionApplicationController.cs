﻿using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static CatGarden.Common.NotificationMessagesConstants;

namespace CatGarden.Web.Controllers
{
    [Authorize]
    public class AdoptionApplicationController : Controller
    {
        private readonly ICatService catService;
        private readonly ICatteryService catteryService;
        private readonly ICatteryOwnerService catteryOwnerService;
        private readonly IAdoptionApplicationService adoptionApplicationService;
        public AdoptionApplicationController(ICatService catService, ICatteryService catteryService, ICatteryOwnerService catteryOwnerService, IAdoptionApplicationService adoptionApplicationService)
        {
            this.catService = catService;
            this.catteryService = catteryService;
            this.catteryOwnerService = catteryOwnerService;
            this.adoptionApplicationService = adoptionApplicationService;
        }

        [HttpPost]
        public async Task<IActionResult> SendApplication(int catId)
        {

            string userId = User.GetId();

            try
            {
                var cat = await catService.GetByIdAsync(catId);
                if (cat == null)
                {
                    TempData[ErrorMessage] = "Cat not found.";
                    return RedirectToAction("All", "Cat");
                }
                var userIsCatteryOwner = await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(userId);
                if (userIsCatteryOwner && !this.User.IsAdmin())
                {
                    TempData[ErrorMessage] = "User is cattery owner.";
                    return RedirectToAction("All", "Cattery");
                }
                if (await adoptionApplicationService.HasUserAlreadySentApplicationForCat(new Guid(userId), catId))
                {
                    TempData[ErrorMessage] = "Adoption application for this cat has already been sent.";
                    return RedirectToAction("Details", "Cat", new { id = catId });
                }
                await adoptionApplicationService.SendApplication(new Guid(userId), catId);
                TempData["SuccessMessage"] = "Adoption application sent successfully!";
                return RedirectToAction("MyApplications", "AdoptionApplication");
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }
        [HttpGet]
        public async Task<IActionResult> MyApplications()
        {
            var userId = User.GetId();

            try
            {
                var applications = await adoptionApplicationService.GetUserAdoptionApplications(new Guid(userId));
                if (applications == null || applications.Count == 0)
                {
                    return View("NoAdoptionApplications");
                }
                return View(applications);
            }
            catch (Exception)
            {
                return GeneralError();
            }

        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.GetId();

            var application = await adoptionApplicationService.GetAdoptionApplicationByIdAsync(id);
            
            if (!await adoptionApplicationService.HasUserAlreadySentApplicationForCat(new Guid(userId), application.CatId))
            {
                TempData[ErrorMessage] = "Adoption application not found.";
                return RedirectToAction("MyApplications", "AdoptionApplication");
            }

            try
            {
                var isDeleted = await adoptionApplicationService.DeleteAdoptionApplicationAsync(id);
                if (isDeleted)
                {
                    TempData[SuccessMessage] = "Adoption application was deleted successfully!";
                }
                else
                {
                    TempData[ErrorMessage] = "Adoption application not found or deletion failed!";
                }

                return RedirectToAction("MyApplications", "AdoptionApplication");
            }
            catch (Exception)
            {
                return GeneralError();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Accept(Guid id)
        {
            var userId = User.GetId()!;
            
            var application = await adoptionApplicationService.GetAdoptionApplicationByIdAsync(id);
            var cat = await catService.GetByIdAsync(application.CatId);
            if (application == null)
            {
                TempData[ErrorMessage] = "Adoption application not found.";
                return RedirectToAction("All", "Catteries");
            }
            if (!await catteryService.IsCatteryOwnedByUserAsync(userId, cat.CatteryId) && !this.User.IsAdmin())
            {
                TempData[ErrorMessage] = "Unauthorized to manage adopt applications!";
                return RedirectToAction("All", "Catteries");
            }
            try
            {
                var result = await adoptionApplicationService.AcceptAdoptionApplicationAsync(id);
                TempData[SuccessMessage] = "Adoption application accepted!";
                return RedirectToAction("Details", "Cattery", new { id = cat.CatteryId });
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Reject(Guid id)
        {
            var userId = User.GetId()!;

            var application = await adoptionApplicationService.GetAdoptionApplicationByIdAsync(id);
            var cat = await catService.GetByIdAsync(application.CatId);
            if (application == null)
            {
                TempData[ErrorMessage] = "Adoption application not found.";
                return RedirectToAction("All", "Catteries");
            }
            if (!await catteryService.IsCatteryOwnedByUserAsync(userId, cat.CatteryId) && !this.User.IsAdmin())
            {
                TempData[ErrorMessage] = "Unauthorized to manage adopt applications!";
                return RedirectToAction("All", "Catteries");
            }
            try
            {
                var result = await adoptionApplicationService.RejectAdoptionApplicationAsync(id);
                TempData[SuccessMessage] = "Adoption application rejected!";
                return RedirectToAction("Details", "Cattery", new { id = cat.CatteryId });
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }
        private IActionResult GeneralError()
        {
            TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator";

            return RedirectToAction("Index", "Home");
        }
    }
}
