using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order
{
   public class Order:EntityBase
    {
        public Order()
        {

        }
        public Order(string buyerEmail,  Address shippingAddress, DeliveryMethod deliverymethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            //OrderDate = orderDate;
            //Status = status;
            ShippingAddress = shippingAddress;
            this.deliverymethod = deliverymethod;
            this.items = items;
            SubTotal = subTotal;
            //PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual Address ShippingAddress { get; set; }

        public virtual DeliveryMethod deliverymethod { get; set; }
        public virtual ICollection<OrderItem> items { get; set; }

        public decimal SubTotal { get; set; }

        public decimal GetTotal()
            => SubTotal + deliverymethod.Cost;
        public string PaymentIntentId { get; set; }=string.Empty;

     
    }
}
