using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Todo.API.Middleware
{
    /// <summary>
    /// Global exception handling middleware that catches unhandled exceptions
    /// and returns standardized error responses.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ProblemDetails();

            switch (exception)
            {
                case InvalidOperationException invalidOpEx:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Status = StatusCodes.Status400BadRequest;
                    response.Title = "Invalid Operation";
                    response.Detail = invalidOpEx.Message;
                    break;

                case UnauthorizedAccessException unauthorizedEx:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Status = StatusCodes.Status401Unauthorized;
                    response.Title = "Unauthorized";
                    response.Detail = "You are not authorized to access this resource.";
                    break;

                case KeyNotFoundException notFoundEx:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response.Status = StatusCodes.Status404NotFound;
                    response.Title = "Not Found";
                    response.Detail = notFoundEx.Message;
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Status = StatusCodes.Status500InternalServerError;
                    response.Title = "Internal Server Error";
                    response.Detail = "An unexpected error occurred. Please try again later.";
                    // In production, don't expose stack trace
                    if (!context.RequestServices.GetRequiredService<IHostEnvironment>().IsProduction())
                    {
                        response.Detail += $" {exception.Message}";
                    }
                    break;
            }

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions 
            { 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
            });

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
