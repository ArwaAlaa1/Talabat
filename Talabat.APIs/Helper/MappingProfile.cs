using AutoMapper;
using Talabat.APIs.DTO;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order;

namespace Talabat.APIs.Helper
{
    public class MappingProfile:Profile
    {
        
        public MappingProfile()
        {
            CreateMap<Product,ProductToReturnDto>().ForMember(b=> b.productBrand,o => o.MapFrom( b=> b.productBrand.Name))
                .ForMember(t => t.productType, o => o.MapFrom(t => t.productType.Name))
                .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductImageResolved>());
            ;

            CreateMap<Talabat.Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDto, Core.Entities.Order.Address>();


        }
    }
}
