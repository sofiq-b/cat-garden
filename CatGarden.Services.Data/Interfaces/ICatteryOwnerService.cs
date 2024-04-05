using CatGarden.Web.ViewModels.CatteryOwner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatGarden.Services.Data.Interfaces
{
    public interface ICatteryOwnerService
    {
        Task<bool> CatteryOwnerExistsByUserIdAsync(string userId);

        Task<bool> CatteryOwnerExistsByPhoneNumberAsync(string phoneNumber);

        Task Create(string userId, BecomeCatteryOwnerFormModel model);
    }
}
