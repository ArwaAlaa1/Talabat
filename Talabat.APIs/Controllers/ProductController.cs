using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.APIs.Helper;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    
    public class ProductController :BaseApiContoller
    {
        private readonly IUnitOfWork unitOfWork;

        //private readonly IGenericRepository<Product> _productrepo;
        //private readonly IGenericRepository<ProductBrand> brandrepo;
        //private readonly IGenericRepository<ProductType> typerepo;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork unitOfWork,
            IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            //_productrepo = productrepo;
            //this.brandrepo = brandrepo;
            //this.typerepo = typerepo;
            this.mapper = mapper;
        }
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.
            AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<PaginationItem<ProductToReturnDto>>> GetAllProducts([FromQuery]ProductSpectParams productSpect)
        {
            var spec = new ProductSpecification(productSpect);
            var products = await unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var CountSpec = new ProductWIthFilterCountSpecification(productSpect);
            var Count = await unitOfWork.Repository<Product>().GetCountWithSpec(CountSpec);
            //var products= await _repository.GetAllAsync();
            return Ok(new PaginationItem<ProductToReturnDto>(productSpect.PageIndex,productSpect.PageSize,Count,data));
        }

        [Authorize]
        [ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductSpecification(id);
            var products = await unitOfWork.Repository<Product>().GetByIdWithSpec(spec);


            //var products = await _repository.GetById(id);

            if (products is null)
                return NotFound(new ApiResponse(404));
            return Ok(mapper.Map<Product,ProductToReturnDto>(products));
        }

        [HttpGet("brands")]
        public async Task<ActionResult> GetAllBrands()
        {
            var brands=await unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult> GetAllTypes()
        {
            var typess = await unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(typess);
        }
    }
}
