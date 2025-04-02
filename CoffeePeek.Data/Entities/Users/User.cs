using Microsoft.AspNetCore.Identity;

namespace CoffeePeek.Data.Entities.Users;

public class User : IdentityUser<int>
{
    public User()
    {
        Reviews = new HashSet<Models.Review.Review>();
    }

    public DateTime CreatedAt { get; set; }
    public string? FullName { get; set; }
    public bool IsSoftDeleted { get; set; }
    
    public ICollection<Models.Review.Review> Reviews { get; set; }
}