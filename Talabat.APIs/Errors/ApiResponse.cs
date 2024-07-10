using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int _statusCode,string? _message=null)
        {
            StatusCode= _statusCode;
            Message= _message ?? GetDefaultMessageForStatusCode(StatusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A Bad Request",
                401 => "Authorized ,you are not ",
                404 => "Resources Not Found",
                500 => "Errors are the path to dark side",
                _ => null
            };
        }

       
    }
}
