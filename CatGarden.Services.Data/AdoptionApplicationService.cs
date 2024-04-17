using CatGarden.Data;
using CatGarden.Data.Models;
using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.ViewModels.AdoptionApplication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using static CatGarden.Common.Enums;

namespace CatGarden.Services.Data
{
    public class AdoptionApplicationService : IAdoptionApplicationService
    {
        private readonly CatGardenDbContext dbContext;
        private readonly ICatService catService;
        public AdoptionApplicationService(CatGardenDbContext dbContext, ICatService catService)
        {
            this.dbContext = dbContext;
            this.catService = catService;
        }

        public async Task SendApplication(Guid userId, int catId)
        {
            var application = new AdoptionApplication
            {
                UserId = userId,
                CatId = catId,
                ApplicationDate = DateTime.Now,
                ApplicationStatus = ApplicationStatus.Pending // Set the default status
            };

            await dbContext.AdoptionApplications.AddAsync(application);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAdoptionApplicationAsync(Guid applicationId)
        {
            var application = await GetAdoptionApplicationByIdAsync(applicationId);

            Cat cat = await catService.GetByIdAsync(application.CatId);
            cat.AdoptionApplications.Remove(application);
            dbContext.AdoptionApplications.Remove(application);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<AdoptionApplicationOfUserViewModel>> GetUserAdoptionApplications(Guid userId)
        {
            var applicationsModels = await dbContext.AdoptionApplications
                  .Where(a => a.UserId == userId)
                  .Select(a => new AdoptionApplicationOfUserViewModel
                  {
                      Id = a.Id.ToString(),
                      Username = a.User.UserName,
                      CatId = a.Cat.Id,
                      CatName = a.Cat.Name,
                      CatteryId = a.Cat.CatteryId, 
                      CatteryName = a.Cat.Cattery.Name,
                      ApplicationDate = a.ApplicationDate,
                      ApplicationStatus = a.ApplicationStatus.ToString(),
                      CoverImageUrl = a.Cat.Images.FirstOrDefault(x => x.IsCover == true)!.URL
                  }).ToListAsync();

            return applicationsModels;
        }


        public async Task<bool> AcceptAdoptionApplicationAsync(Guid id)
        {
            var application = await GetAdoptionApplicationByIdAsync(id);

            application.ApplicationStatus = ApplicationStatus.Accepted;
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectAdoptionApplicationAsync(Guid id)
        {
            var application = await GetAdoptionApplicationByIdAsync(id);

            application.ApplicationStatus = ApplicationStatus.Rejected;
            await dbContext.SaveChangesAsync();
            return true;
        }







        public async Task<AdoptionApplication> GetAdoptionApplicationByIdAsync(Guid applicationId)
        {
            return await dbContext.AdoptionApplications.FindAsync(applicationId);
        }

        public async Task<bool> HasUserAlreadySentApplicationForCat(Guid userId, int catId)
        {
            return await dbContext.AdoptionApplications
                                  .AnyAsync(app => app.UserId == userId && app.CatId == catId);
        }


    }
}
