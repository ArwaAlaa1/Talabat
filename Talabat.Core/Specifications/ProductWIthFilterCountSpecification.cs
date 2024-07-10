using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWIthFilterCountSpecification:BaseSpecification<Product>
    {
        
        public ProductWIthFilterCountSpecification(ProductSpectParams productSpect)
            : base(P =>
            (string.IsNullOrEmpty(productSpect.Search) || P.Name.ToLower().Contains(productSpect.Search)) &&
        (!productSpect.BrandId.HasValue || P.ProductBrandId == productSpect.BrandId) &&
        (!productSpect.TypeId.HasValue || P.ProductTypeId == productSpect.TypeId))
        {
           
        }

    }
}
