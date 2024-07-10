using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Extentions
{
    public static class UserMnagerExtentions
    {
        public static async Task<AppUser> FindUserAddressByEmailAsync(this UserManager<AppUser> userManager, ClaimsPrincipal CurrentUser)
        {
            var email=CurrentUser.FindFirstValue(ClaimTypes.Email);
            var user=await userManager.Users.Include(a=> a.Address).FirstOrDefaultAsync(u=>u.Email==email);

            return user;
        
        }
    }
}
