using System.Security.Claims;
using CoffeePeek.BusinessLogic.Constants;

namespace CoffeePeek.Api.Middleware;

public class UserTokenMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                
                context.Items[AuthConfig.JWTTokenUserPropertyName] = userId;
            }
        }

        await next(context);
    }
}