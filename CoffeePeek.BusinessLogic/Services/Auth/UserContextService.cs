using CoffeePeek.BusinessLogic.Constants;
using Microsoft.AspNetCore.Http;

namespace CoffeePeek.BusinessLogic.Services.Auth;

public class UserContextService(IHttpContextAccessor httpContextAccessor) : IUserContextService
{
    public bool TryGetUserId(out int userId)
    {
        userId = 0;
        var context = httpContextAccessor.HttpContext;
        return context?.Items.TryGetValue(AuthConfig.JWTTokenUserPropertyName, out var userIdObj) == true
               && userIdObj is int id
               && (userId = id) > 0;
    }
}