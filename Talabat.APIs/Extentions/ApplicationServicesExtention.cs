using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Runtime.CompilerServices;
using Talabat.APIs.Errors;
using Talabat.APIs.Helper;
using Talabat.Core;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Service;

namespace Talabat.APIs.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<IUnitOfWork,UnitOfWork>();
            Services.AddScoped<IOrderServices, OrderServices>();
            Services.AddScoped<IPaymentServices, PaymentServices>();

            //Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //WebApplicationbuilder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfile( )));
            Services.AddAutoMapper(typeof(MappingProfile));


            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (optioncontext) =>
                {
                    var errors = optioncontext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                              .SelectMany(p => p.Value.Errors)
                                              .Select(p => p.ErrorMessage).ToList();

                    var ValidationErrorResponse = new ApiValidationError()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };

            });
             Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            return Services;

        }
    }
}
