using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{
   
    public class BasketController : BaseApiContoller
    {
        private readonly IBasketRepository basketRepo;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository basketRepo,IMapper mapper)
        {
            this.basketRepo = basketRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketAsync(string id)
        {
           var basket= await basketRepo.GetBasketAsync(id);
            return basket is null ? new CustomerBasket(id) : basket;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto basket)
        {
            var mappedcustomerbasket=mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
            var CreateOrUpdateBasket =await basketRepo.UpdateBasketAsync(mappedcustomerbasket);
            if (CreateOrUpdateBasket is null) return BadRequest(new ApiResponse(400));
            return Ok(CreateOrUpdateBasket);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string id)
        {
            return await basketRepo.DeleteBasketAsync(id);
        }
    }
}
