namespace CoffeePeek.Data.Repositories.InReview;

public interface IReviewShopsRepository
{
    Task<bool> UpdatePhotos(int shopId, int userId, ICollection<string> urls);
}