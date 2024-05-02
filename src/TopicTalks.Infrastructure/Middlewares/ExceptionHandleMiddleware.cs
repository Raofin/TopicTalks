using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace TopicTalks.Infrastructure.Middlewares;

public class ExceptionHandleMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleException(ex, httpContext);
        }
    }

    private async Task HandleException(Exception ex, HttpContext httpContext)
    {
        Log.Error(ex, "An error occurred while processing the request.");

        httpContext.Response.StatusCode = ex switch
        {
            ArgumentNullException or ArgumentException => 400,
            UnauthorizedAccessException => 403,
            KeyNotFoundException => 404,
            _ => 500
        };

        var errorResponse = new ErrorResponse {
            Message = ex.Message,
            Details = ex.StackTrace
        };

        await httpContext.Response.WriteAsJsonAsync(errorResponse);
    }

    private class ErrorResponse
    {
        public string? Message { get; set; }
        public string? Details { get; set; }
    }
}
