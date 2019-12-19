using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DiplomaSurvive.Api
{
    public class ExceptionMiddleWare
    {/// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate _next;

        private static ILogger<ExceptionMiddleWare> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleWare"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns>
        /// The method is void.
        /// </returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Handles the exception asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>
        /// The method is void.
        /// </returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            _logger.LogCritical("Time: {date} Message: {message} Inner: {inner} Exception: {exception} Trace: {trace}", DateTime.UtcNow, exception.Message, exception.InnerException?.Message, exception, exception.StackTrace);
            
            ApiResponse error = new ApiResponse
            {
                Result = false,
                Errors = new string[]
                {
                    "Just don’t worry, I’m already calling my developers to address this problem"
                }
            };

            return context.Response.WriteAsync(error.ToString());
        }
    }
}