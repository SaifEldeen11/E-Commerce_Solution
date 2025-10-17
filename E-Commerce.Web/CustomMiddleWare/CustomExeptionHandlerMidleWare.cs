using Domain.Exeptions;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.Web.CustomMiddleWare
{
    public class CustomExeptionHandlerMidleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExeptionHandlerMidleWare> _logger;

        public CustomExeptionHandlerMidleWare(RequestDelegate next,ILogger<CustomExeptionHandlerMidleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // request

                await _next.Invoke(httpContext);

                // response

                await HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");

                // Set Status Code for the response
                await HandleExeptionAsync(httpContext, ex);

            }
        }

        private static async Task HandleExeptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundExeption => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorToReturn()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message

            };

            //var responeJson = System.Text.Json.JsonSerializer.Serialize(response); // convert object to json

            //await httpContext.Response.WriteAsync(responeJson);

            await httpContext.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpContext.Request.Path} is not found "
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
