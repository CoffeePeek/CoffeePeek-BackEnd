namespace CoffeePeek.BusinessLogic.Services;

public interface IHashingService
{
    string HashString(string password);
    bool VerifyHashedStrings(string value, string hashedValue);
    string GenerateRandomPassword(int length = 12);
}