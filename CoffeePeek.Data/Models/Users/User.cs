namespace CoffeePeek.Data.Models.Users;

public class User : BaseModel
{
    public User()
    {
        Reviews = new HashSet<Review.Review>();
    }

    public string UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    /// <summary>
    /// Hash Password
    /// </summary>
    public string Password { get; set; }
    
    public bool IsSoftDeleted { get; set; }
    
    public ICollection<Review.Review> Reviews { get; set; }
}