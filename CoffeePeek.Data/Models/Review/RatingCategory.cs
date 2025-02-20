using CoffeePeek.Contract.Enums;

namespace CoffeePeek.Data.Models.Review;

public class RatingCategory : BaseModel
{
    public RatingCategoryType Type { get; set; }
}