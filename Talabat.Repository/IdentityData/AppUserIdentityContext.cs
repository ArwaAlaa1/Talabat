using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.IdentityData
{
    public class AppUserIdentityContext:IdentityDbContext<AppUser>
    {
        public AppUserIdentityContext(DbContextOptions<AppUserIdentityContext> options):base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }

    }
}
