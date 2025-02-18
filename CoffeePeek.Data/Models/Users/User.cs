using Microsoft.AspNetCore.Identity;

namespace CoffeePeek.Data.Models.Users;

public class User : IdentityUser<int>
{
    public User()
    {
        Reviews = new HashSet<Review.Review>();
    }

    public DateTime CreatedAt { get; set; }
    public string? FullName { get; set; }
    public bool IsSoftDeleted { get; set; }
    
    public ICollection<Review.Review> Reviews { get; set; }
}