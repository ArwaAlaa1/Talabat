using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order;

namespace Talabat.Core.Specifications.OrderSpecification
{
    public class OrderSpecification:BaseSpecification<Order>
    {
        public OrderSpecification(string email)
            :base(o=>o.BuyerEmail==email)
        {
            includes.Add(o => o.deliverymethod);
            includes.Add(o => o.items);

            AddOrderByDescending(o => o.OrderDate);
        }

        public OrderSpecification(int id,string email)
           : base(o => o.BuyerEmail == email && o.Id==id)
        {
            includes.Add(o => o.deliverymethod);
            includes.Add(o => o.items);

          
        }
    }
}
