using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{
    public class TokenServices : ITokenService
    {
        private readonly IConfiguration config;

        public TokenServices(IConfiguration config)
        {
            this.config = config;
        }
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            var authoClaims = new List<Claim>()
            {
              new Claim(ClaimTypes.GivenName,user.DisplayName),
              new Claim(ClaimTypes.Email,user.Email),

            };
            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
                authoClaims.Add(new Claim(ClaimTypes.Role, role));

            var authoKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: config["JWT:ValidIssuers"],
                audience: config["JWT:ValidAudieus"],
                expires:DateTime.Now.AddDays(double.Parse(config["JWT:DurationInDays"])),
                claims:authoClaims,
                signingCredentials:new SigningCredentials(authoKey,SecurityAlgorithms.HmacSha256Signature)

                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
