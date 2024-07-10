using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.APIs.Controllers
{
   
    public class AccountController : BaseApiContoller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager
            ,ITokenService tokenService,IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user=await userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new ApiResponse(401));

            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto()
            {
                DisplayName=user.DisplayName, 
                Email=model.Email,
                Token=await tokenService.CreateTokenAsync(user,userManager)
                
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExsist(model.Email).Result.Value)
                return BadRequest(new ApiValidationError() { Errors = new string[] { "This Email is Already Exist" } });
            var user = new AppUser()
            {
                DisplayName=model.DisplayName,
                Email=model.Email,
                UserName = model.Email.Split("@")[0],
                PhoneNumber=model.PhoneNumber
            };

            var result=await userManager.CreateAsync(user,model.PassWord);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto()
            {
                DisplayName=user.DisplayName,
                Email=model.Email,
                Token=await tokenService.CreateTokenAsync(user,userManager)

            });
        }

      [Authorize]
        [HttpGet("currentuser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
           var email = User.FindFirstValue(ClaimTypes.Email);

            var user=await userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.CreateTokenAsync(user, userManager)
            });
        }



        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetAddressUser()
        {
            var user = await userManager.FindUserAddressByEmailAsync(User);
            var address=mapper.Map<Address, AddressDto>(user.Address);
            return Ok(address);
           
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateAddressUser(AddressDto updatedaddress)
        {
            
            var address = mapper.Map<AddressDto,Address >(updatedaddress);
            var user = await userManager.FindUserAddressByEmailAsync(User);

            address.Id = user.Address.Id;
            user.Address = address;
            var result=await userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(updatedaddress);

        }

        [HttpGet("checkemail")]
        public async Task<ActionResult<bool>> CheckEmailExsist(string email)
        {
            return await userManager.FindByEmailAsync(email) is not null;
        }


    }
}
