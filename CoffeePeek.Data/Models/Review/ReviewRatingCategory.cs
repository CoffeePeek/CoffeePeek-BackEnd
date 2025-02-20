using CoffeePeek.Contract.Enums;

namespace CoffeePeek.Data.Models.Review;

public class ReviewRatingCategory
{
    public int Id { get; set; }
    
    public int ReviewId { get; set; }
    public int RatingCategoryId { get; set; }

    public Review Review { get; set; }
    public RatingCategory RatingCategory { get; set; }

    public RatingCategoryValue Value { get; set; }
}