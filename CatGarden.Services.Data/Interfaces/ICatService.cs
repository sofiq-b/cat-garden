﻿using CatGarden.Data.Models;
using CatGarden.ViewModels.Cat;
using CatGarden.Web.ViewModels.Cat;
using CatGarden.Web.ViewModels.Home;

namespace CatGarden.Services.Data.Interfaces
{
    public interface ICatService
    {
        Task<IEnumerable<IndexViewModel>> LastThreeCatsAsync();

        Task<int> CreateAndReturnIdAsync(CatFormModel formModel);

        Task<bool> ExistsByIdAsync(int catId);

        Task<CatDetailsViewModel> GetDetailsByIdAsync(int catId, string userId);

        Task AddCatToFavoritesAsync(int catId, string userId);
        Task RemoveFavoriteAsync(int catId, string userId);
        Task<CatDisplayViewModel> GetCatDisplayViewModelAsync(int catId);

        Task<IEnumerable<CatDisplayViewModel>> GetFavoriteCatsAsync(string userId);

        Task RemoveAllCatsFromFavoritesAsync(string userId);

        Task<IEnumerable<CatDisplayViewModel>> GetAllCatsAsync(string userId);

        Task UpdateCatLikesAsync(Cat cat);

        Task<Cat> GetByIdAsync(int catId);
        Task<CatFormModel> GetCatForEdit(int catId, string userId);
        Task<bool> IsCatPartOfOwnedCattery(int catId, string ownerId);
        Task<bool> IsFavoritedByUserWithIdAsync(int catId, string userId);

    }
}
