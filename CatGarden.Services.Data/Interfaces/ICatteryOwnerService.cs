using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatGarden.Services.Data.Interfaces
{
    public interface ICatteryOwnerService
    {
        Task<bool> CatteryOwnerExistsByUserId(string userId);
    }
}
