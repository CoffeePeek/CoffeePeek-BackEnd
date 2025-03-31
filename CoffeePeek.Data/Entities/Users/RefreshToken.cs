using CoffeePeek.Data.Entities;
using CoffeePeek.Data.Entities.Users;

namespace CoffeePeek.Data.Models.Users;

public class RefreshToken : BaseEntity
{
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }

    public User User { get; set; }
}