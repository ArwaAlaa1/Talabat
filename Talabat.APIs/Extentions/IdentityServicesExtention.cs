using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;
using Talabat.Repository.IdentityData;
using Talabat.Service;

namespace Talabat.APIs.Extentions
{
    public static class IdentityServicesExtention
    {

        public static IServiceCollection AddIdentityServices(this IServiceCollection Services,IConfiguration config)
        {
            Services.AddScoped<ITokenService, TokenServices>();
            Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric=true;
                options.Password.RequireLowercase=true; 
                options.Password.RequireUppercase=true;
                options.Password.RequireDigit = true;

            }).AddEntityFrameworkStores<AppUserIdentityContext>();

            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                
            })
                 .AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateAudience = true,
                     ValidateIssuer = true,
                     ValidIssuer = config["JWT:ValidIssuers"],
                     ValidAudience = config["JWT:ValidAudieus"],
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"])),
                 }
                 );

            return Services;


        }
    }
}
