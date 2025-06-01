using System.Net;
using System.Text.Json;

namespace MDDPlatform.DomainModels.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex.Message}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var statusCode = context.Response.StatusCode;
        var message = "MDDPlatform Domain Models Service Failed";

        await context.Response.WriteAsync(
            new ErrorDetails(statusCode,message).ToString()
        );
    }
}
public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public ErrorDetails(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}