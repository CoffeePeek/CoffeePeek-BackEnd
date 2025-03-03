namespace CoffeePeek.BusinessLogic.Services.Auth;

public interface IUserContextService
{
    bool TryGetUserId(out int userId);
}