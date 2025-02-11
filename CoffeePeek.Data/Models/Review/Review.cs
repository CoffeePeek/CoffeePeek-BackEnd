using CoffeePeek.Data.Models.Users;

namespace CoffeePeek.Data.Models.Review;

public class Review : BaseModel
{
    public string Header { get; set; }
    public string Comment { get; set; }
    public int UserId { get; set; }

    public virtual User User { get; set; }
}