using System;
using System.Net;
using System.Text.Json;

namespace UserService.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex}. Request failed with Status Code {(int)HttpStatusCode.InternalServerError}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new { error = $"An error occured while processing your request.\n{ex.Message}" });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            
            
            return context.Response.WriteAsync(result);
        }
    }
}
