using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.APIs.Helper;
using Talabat.APIs.Middlewares;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.IdentityData;

namespace Talabat.APIs
{
    public class Program
    {
        public static  async Task Main(string[] args)
        {
            var WebApplicationbuilder = WebApplication.CreateBuilder(args);

            #region Configure services 
            // Add services to the container.

            WebApplicationbuilder.Services.AddControllers();
            //register required web api services to thr DI Container 

            WebApplicationbuilder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(WebApplicationbuilder.Configuration.GetConnectionString("DefaultConnection"));
            });

            WebApplicationbuilder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(WebApplicationbuilder.Configuration.GetConnectionString("DefaultConnection"));
            });
            WebApplicationbuilder.Services.AddDbContext<AppUserIdentityContext>(options =>
            {
                options.UseSqlServer(WebApplicationbuilder.Configuration.GetConnectionString("IdentityConnection"));
            });

            WebApplicationbuilder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connection = WebApplicationbuilder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);
            });
            WebApplicationbuilder.Services.AddApplicationServices();

            WebApplicationbuilder.Services.AddIdentityServices(WebApplicationbuilder.Configuration);

            WebApplicationbuilder.Services.AddSwaggerServices();


            //register required of swagger that make documentation  
            #endregion

            var app = WebApplicationbuilder.Build();

            #region Update DB

            //Explicitly
            var scope = app.Services.CreateScope();//call all services that work scopped
            var services = scope.ServiceProvider;//DI over services scopped (make object for all services scopped)
            //logger factury to log exception in console

            var loggerfactury = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<StoreContext>(); //ask clr to create object from store context explicitly 
                await dbContext.Database.MigrateAsync();
                await StoreContextSeeding.SeedAsync(dbContext);

                var IdentityContext = services.GetRequiredService<AppUserIdentityContext>(); //ask clr to create object from store context explicitly 
                await IdentityContext.Database.MigrateAsync();

                var usermanager=services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDBContextSeeding.SeedUserAsync(usermanager);

            }
            catch (Exception ex)
            {
                var logger = loggerfactury.CreateLogger<Program>();//return to main 
                logger.LogError(ex, "Erro Occured during apply migration");

            }

            #endregion
            #region Configuration middleswares

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }
            app.UseMiddleware<ExceptionMiddleWares>();
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}