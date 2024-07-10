using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Talabat.APIs.Middlewares
{
    public class ExceptionMiddleWares
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleWares> logger;
        private readonly IHostEnvironment environment;

        public ExceptionMiddleWares(RequestDelegate next,ILogger<ExceptionMiddleWares> logger,IHostEnvironment environment)
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                httpContext.Response.ContentType= "application/json";
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

               
                var response = environment.IsDevelopment() ?
                       new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                       :
                        new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json =JsonSerializer.Serialize(response,options);
                await httpContext.Response.WriteAsync(json);

            }
        }
    }
}
