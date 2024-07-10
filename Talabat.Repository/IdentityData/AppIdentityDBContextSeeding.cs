using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.IdentityData
{
    public static class AppIdentityDBContextSeeding
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName="Arwa Alaa",
                    Email="arwaalaa1268@gmail.com",
                    UserName= "arwaalaa1268",
                    PhoneNumber="01011037481"
                };

                await userManager.CreateAsync(user,"P@ssW5rd");
            }

           
        } 
    }
}
