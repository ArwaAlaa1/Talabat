using Talabat.Core.Entities.Order;

namespace Talabat.APIs.DTO
{
    public class OrderDto
    {

        public string BasketId { get; set; }
        public virtual AddressDto ShippingAddress { get; set; }

        public virtual int DeliveryMethodId { get; set; }
        
    }
}
