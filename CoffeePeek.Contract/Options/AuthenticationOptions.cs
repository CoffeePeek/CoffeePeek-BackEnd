namespace CoffeePeek.Contract.Options;

public class AuthenticationOptions
{
    public string JwtSecretKey { get; set; }
    public int ExpireIntervalMinutes { get; set; }
    public int ExpireRefreshIntervalDays { get; set; }
}