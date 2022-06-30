using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MovieShopMVC.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MovieShopExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<MovieShopExceptionMiddleware> _logger;

        public MovieShopExceptionMiddleware(RequestDelegate next, ILogger<MovieShopExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                // read info from the HttpContext object and log it some where
                _logger.LogInformation("Inside the Middleware");
                await _next(httpContext);   //was "return _next(httpContext);"
            }
            catch (Exception ex)
            {
                // First catch the exception :
                // chek the exception type, message,
                // check stacktrace, where the exception happned
                // when the exception happened
                // for which URL and which Http method (controller, action method) happned
                // for which user, if user is loged in

                var exceptionDetails = new ErrorModel
                {
                    Message = ex.Message,
                    //StackTrace = ex.StackTrace,
                    ExceptionDateTime = DateTime.UtcNow,
                    ExceptionType = ex.GetType().ToString(),
                    Path = httpContext.Request.Path,
                    HttpRequestType = httpContext.Request.Method,
                    User = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : null
                };

                // Once we got this info => need to Save all this information some where like text files, json files or Database
                // use Sytem.IO to craete text files (we don't need to use this one)
                // asp.net core has builtin logging mechanism, (ILogger) which can be used by any 3rd party log provide
                // *SeriLog* and NLog
                // Send email to Dev Team when exception happens support@antra.com

                _logger.LogError("Exception happened, log this to text or Json files using SeriLog");

                // return to HomeController and Error Action method
                // error() method will return Error view

                httpContext.Response.Redirect("/home/error");

            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MovieShopExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareClassTemplate(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieShopExceptionMiddleware>();
        }
    }
}

