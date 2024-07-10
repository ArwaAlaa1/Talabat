using AutoMapper;
using Talabat.APIs.DTO;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helper
{
    public class ProductImageResolved : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductImageResolved(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{configuration["BaseUrl"]}/{source.PictureUrl}";

            return string.Empty ;
        }
    }
}
