﻿using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static CatGarden.Common.GeneralApplicationConstants;

namespace CatGarden.Web.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;
        private readonly IMemoryCache memoryCache;

        public UserController(IUserService userService, IMemoryCache memoryCache)
        {
            this.userService = userService;
            this.memoryCache = memoryCache;
        }

        [Route("User/All")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client, NoStore = false)]
        public async Task<IActionResult> All()
        {
            IEnumerable<UserViewModel> users =
                this.memoryCache.Get<IEnumerable<UserViewModel>>(UsersCacheKey);
            if (users == null)
            {
                users = await this.userService.AllAsync();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan
                        .FromMinutes(UsersCacheDurationMinutes));

                this.memoryCache.Set(UsersCacheKey, users, cacheOptions);
            }

            return View(users);
        }
    }
}
