using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductSpecification:BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpectParams productSpect)
            :base(P => 
            (string.IsNullOrEmpty(productSpect.Search) || P.Name.ToLower().Contains(productSpect.Search))&&
        (!productSpect.BrandId.HasValue || P.ProductBrandId == productSpect.BrandId) &&
        (!productSpect.TypeId.HasValue  || P.ProductTypeId  == productSpect.TypeId))
        {
            includes.Add(p => p.productBrand);
            includes.Add(p => p.productType);

           
            if (!string.IsNullOrEmpty(productSpect.Sort))
            {
                switch (productSpect.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDes":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

            AddPagination(productSpect.PageSize * (productSpect.PageIndex - 1),productSpect.PageSize);
            
            
        }

        public ProductSpecification(int id ):base(p=>p.Id==id) 
        {
            includes.Add(p => p.productBrand);
            includes.Add(p => p.productType);

        }
    }
}
