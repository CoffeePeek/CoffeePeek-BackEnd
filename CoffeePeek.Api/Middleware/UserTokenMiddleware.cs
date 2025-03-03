using System.Security.Claims;
using CoffeePeek.BusinessLogic.Constants;

namespace CoffeePeek.Api.Middleware;

public class UserTokenMiddleware(RequestDelegate next, ILogger<UserTokenMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity is not { IsAuthenticated: true })
        {
            var claims = context.User.Claims.ToList();
            if (claims.Any())
            {
                var claimsIdentity = new ClaimsIdentity(claims, "Bearer");
                context.User = new ClaimsPrincipal(claimsIdentity);
            }
        }

        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                logger.LogInformation($"Extracted User ID: {userId}");
                context.Items[AuthConfig.JWTTokenUserPropertyName] = userId;
            }
            else 
            {
                logger.LogWarning("Failed to parse User ID from claims");
            }
        }
        else 
        {
            logger.LogWarning("User is not authenticated");
        }

        await next(context);
    }
}