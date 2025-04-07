using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Entities.Shop;
using CoffeePeek.Data.Models.Shop;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.Data.Repositories.InReview;

public class ReviewShopsRepository(CoffeePeekDbContext context) : Repository<ReviewShop>(context), IReviewShopsRepository
{
    public async Task<bool> UpdatePhotos(int shopId, int userId, ICollection<string> urls)
    {
        var shop = await context.ReviewShops.FirstOrDefaultAsync(x => x.Id == shopId);
        
        if (shop == null)
        {
            return false;
        }

        var photos = urls.Select(x => new ShopPhoto
        {
            CreatedAt = DateTime.Now,
            Url = x,
            ShopId = shopId,
            UserId = userId,
        });

        foreach (var photo in photos)
        {
            shop.ShopPhotos.Add(photo);   
        }
        
        await context.SaveChangesAsync();
        
        return true;
    }
}