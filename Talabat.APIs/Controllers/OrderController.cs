using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Order;
using Talabat.Core.Services.Contract;
using Talabat.Service;

namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class OrderController : BaseApiContoller
    {

        private readonly IOrderServices orderservice;
        private readonly IMapper mapper;

        public OrderController(IOrderServices orderservice, IMapper mapper)
        {

            this.orderservice = orderservice;
            this.mapper = mapper;
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var buyeremail = User.FindFirstValue(ClaimTypes.Email);
            var ddress = mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);

            var order = await orderservice.CreateOrderAsync(buyeremail, orderDto.BasketId, orderDto.DeliveryMethodId, ddress);

            if (order is null) return BadRequest(new ApiResponse(400));
            return Ok(order);

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
        {
            var buyeremail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await orderservice.GetOrdersForSpecificUserAsync(buyeremail);

            return Ok(orders);
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("id")]
        public async Task<ActionResult<Order>> GetOrderByIdForUser(int id)
        {
            var buyeremail = User.FindFirstValue(ClaimTypes.Email);
            var order = await orderservice.GetOrderByIdForSpecificUser(id, buyeremail);
            if (order is null) return NotFound(new ApiResponse(404));
            return Ok(order);
        }

        [HttpGet("deliverymethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
            {
            var deliverymethod = await orderservice.GetDeliveryMethodAsync();
            return Ok(deliverymethod);
            }



    }
}
