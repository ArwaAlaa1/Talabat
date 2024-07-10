using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order;

namespace Talabat.Core.Services.Contract
{
    public interface IOrderServices
    {
        Task<Order?> CreateOrderAsync(string BuyerEmail,string BasketId,int DeliveryMethodId,Address ShippingAddress);
        Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail);
        Task<Order> GetOrderByIdForSpecificUser(int OrderId,string  BuyerEmail );
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
    }
}
