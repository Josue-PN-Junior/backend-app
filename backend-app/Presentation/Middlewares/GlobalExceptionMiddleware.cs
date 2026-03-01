using System.Net;
using System.Text.Json;
using backend_app.Helpers.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during execution.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        int statusCode;
        string message;
        string? details = null;

        switch (exception)
        {
            case BaseCustomException customEx:
                statusCode = customEx.StatusCode;
                message = _environment.IsDevelopment() ? customEx.DetailedMessage : customEx.ProductionMessage;
                details = _environment.IsDevelopment() ? exception.StackTrace : null;
                break;

            case DbUpdateConcurrencyException:
                statusCode = 409;
                message = _environment.IsDevelopment() ? exception.Message : "Data conflict.";
                details = _environment.IsDevelopment() ? exception.StackTrace : null;
                break;

            case DbUpdateException:
                statusCode = 500;
                message = _environment.IsDevelopment() ? exception.Message : "Internal server error.";
                details = _environment.IsDevelopment() ? exception.StackTrace : null;
                break;


            case ArgumentException:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = _environment.IsDevelopment() ? exception.Message : "Invalid data provided.";
                details = _environment.IsDevelopment() ? exception.StackTrace : null;
                break;

            case KeyNotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                message = _environment.IsDevelopment() ? exception.Message : "Resource not found.";
                details = _environment.IsDevelopment() ? exception.StackTrace : null;
                break;

            case UnauthorizedAccessException:
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = _environment.IsDevelopment() ? exception.Message : "Unauthorized access.";
                details = _environment.IsDevelopment() ? exception.StackTrace : null;
                break;

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = _environment.IsDevelopment() ? exception.Message : "Internal server error.";
                details = _environment.IsDevelopment() ? exception.StackTrace : null;
                break;
        }

        context.Response.StatusCode = statusCode;

        var response = new
        {
            StatusCode = statusCode,
            Message = message,
            Details = details
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
