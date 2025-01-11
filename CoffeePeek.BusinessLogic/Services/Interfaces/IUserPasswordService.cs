namespace CoffeePeek.BusinessLogic.Services;

public interface IUserPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
    string GenerateRandomPassword(int length = 12);
}