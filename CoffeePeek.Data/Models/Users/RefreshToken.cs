namespace CoffeePeek.Data.Models.Users;

public class RefreshToken : BaseModel
{
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }

    public User User { get; set; }
}