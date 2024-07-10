using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketRepository basketRepository;
        private readonly IConfiguration configuration;

        public PaymentServices(IUnitOfWork unitOfWork,IBasketRepository basketRepository,IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.basketRepository = basketRepository;
            this.configuration = configuration;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = configuration["StripSetting:SecretKey"];
            var basket = await basketRepository.GetBasketAsync(basketId);

            if (basket is null) return null;
            var shippingPrice = 0m;
            if (basket.deliverymethodId.HasValue)
            {
                var DELIVERYMETHOD = await unitOfWork.Repository<DeliveryMethod>().GetById(basket.deliverymethodId.Value);
                shippingPrice = DELIVERYMETHOD.Cost;
                basket.ShippingCost = DELIVERYMETHOD.Cost;

            }

            if (basket?.Items?.Count>0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await unitOfWork.Repository<Core.Entities.Product>().GetById(item.Id);
                    if(item.Price != product.Price)
                        item.Price = product.Price;
                }
            }

            var services=new PaymentIntentService();    
            PaymentIntent paymentIntent;
            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) * (long)shippingPrice * 100,
                    Currency="usd",
                    PaymentMethodTypes=new List<string>() { "card"},
                    
                };
                paymentIntent=await services.CreateAsync(options);
                basket.PaymentIntentId=paymentIntent.Id;
                basket.ClientSecret=paymentIntent.ClientSecret;

            }else{
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) * (long)shippingPrice * 100,
                    
                };
                await services.UpdateAsync(basket.PaymentIntentId, options);

            }

            await basketRepository.UpdateBasketAsync(basket);
            return basket;
        }   
    }
}
