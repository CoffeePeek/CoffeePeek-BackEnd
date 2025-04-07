using ClassLibrary.SharedModels;
using CoffeePeek.Data.Repositories.InReview;
using MassTransit;

namespace CoffeePeek.Infrastructure.Consumers;

public class PhotoUploadResultConsumer(IReviewShopsRepository repository) : IConsumer<IPhotoUploadResult>
{
    public async Task Consume(ConsumeContext<IPhotoUploadResult> context)
    {
        await repository.UpdatePhotos(context.Message.ShopId, context.Message.UserId, context.Message.PhotoUrls);
    }
}