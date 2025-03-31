using CoffeePeek.Data.Entities;
using CoffeePeek.Data.Enums;

namespace CoffeePeek.Data.Models.Review;

public class RatingCategory : BaseEntity
{
    public RatingCategoryType Type { get; set; }
}