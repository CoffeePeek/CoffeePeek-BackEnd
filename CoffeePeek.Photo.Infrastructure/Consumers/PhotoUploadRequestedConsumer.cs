using CoffeePeek.Photo.Core.Interfaces;
using CoffeePeek.Shared.Models.PhotoUpload;
using MassTransit;

namespace CoffeePeek.Photo.Infrastructure.Consumers;

public class PhotoUploadRequestedConsumer(IPhotoStorage photoStorage) : IConsumer<IPhotoUploadRequested>
{
    public async Task Consume(ConsumeContext<IPhotoUploadRequested> context)
    {
        var uniqKey = $"{Guid.NewGuid()}-{context.Message.UserId}-{context.Message.ShopId}";

        var uploadTasks = context.Message.Photos.Select(x => photoStorage.UploadPhotoAsync(x, uniqKey)).ToList();

        await Task.WhenAll(uploadTasks);

        var photoUrls = uploadTasks.Select(x => x.Result).ToList();
        
        await context.Publish<IPhotoUploadResult>(new
        {
            context.Message.UserId,
            context.Message.ShopId,
            PhotoUrls = photoUrls
        });
    }
}