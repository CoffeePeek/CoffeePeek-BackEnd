using System.Security.Claims;
using CoffeePeek.Data.Models.Users;

namespace CoffeePeek.BusinessLogic.Services.Auth;

public interface IAuthService
{
    Task<string> GenerateToken(User user);
    string GenerateRefreshToken(int userId);
    int? DecryptRefreshToken(string refreshToken);
}