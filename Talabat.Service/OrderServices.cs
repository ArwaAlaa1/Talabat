using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
using Talabat.Core.Specifications.OrderSpecification;

namespace Talabat.Service
{
    public class OrderServices : IOrderServices
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;

        //private readonly IGenericRepository<Product> genericRepository;
        //private readonly IGenericRepository<DeliveryMethod> deliveryrepo;
        //private readonly IGenericRepository<Order> orderrepo;

        public OrderServices(IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
            //this.genericRepository = genericRepository;
            //this.deliveryrepo = deliveryrepo;
            //this.orderrepo = orderrepo;
        }
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            var basket=await basketRepository.GetBasketAsync(BasketId);
            var orderitems = new List<OrderItem>();

            if (basket.Items.Count>0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await unitOfWork.Repository<Product>().GetById(item.Id);
                    var productitemorder = new ProductOrderItem(product.Id, product.Name, product.PictureUrl);
                    var orderitem = new OrderItem(productitemorder, product.Price, item.Quantity);

                    orderitems.Add(orderitem);
                }
            }

            var subtotal=orderitems.Sum(i=>i.Price*i.Quantity);

            var deliverymethod=await unitOfWork.Repository<DeliveryMethod>().GetById(DeliveryMethodId);

            var order = new Order(BuyerEmail, ShippingAddress, deliverymethod,orderitems, subtotal);

            await unitOfWork.Repository<Order>().Add(order);

            var result=await unitOfWork.Complet();

            if (result<=0) return null;

            return order;
            
            
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            var deliverymethod = await unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliverymethod;
        }

        public async Task<Order> GetOrderByIdForSpecificUser(int OrderId, string BuyerEmail)
        {
            var spec = new OrderSpecification(OrderId, BuyerEmail);

            var order =await unitOfWork.Repository<Order>().GetByIdWithSpec(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail)
        {
            var spec = new OrderSpecification(BuyerEmail);
           var orders=await unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }
    }
}
