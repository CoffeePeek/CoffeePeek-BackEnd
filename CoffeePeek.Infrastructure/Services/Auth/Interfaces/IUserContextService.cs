namespace CoffeePeek.Infrastructure.Services.Auth.Interfaces;

public interface IUserContextService
{
    bool TryGetUserId(out int userId);
}