using CoffeePeek.Contract.Response;
using Microsoft.AspNetCore.Http;
using Utilities.Exceptions;

namespace Utilities.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
        catch (NpgsqlException ex)
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsJsonAsync(new Response<object> { Success = false, Message = ex.Message });
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new Response<object> { Success = false, Message = ex.Message });
        }
        catch (UnauthorizedAccessException)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new Response<object> { Success = false, Message = "Unauthorized access." });
        }
            
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled Exception: {ex}");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new Response<object> { Success = false, Message = "An error occurred while processing your request." });
        }
    }
}