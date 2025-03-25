using CoffeePeek.Photo.Application.Interfaces;
using CoffeePeek.Photo.Application.Messaging;
using Microsoft.AspNetCore.Http;

namespace CoffeePeek.Photo.Application.Services;

public class PhotoUploadService(IPhotoStorage photoStorage, IRabbitMqPublisher rabbitMqPublisher) : IPhotoUploadService
{
    public async Task<string> UploadPhotoAsync(IFormFile requestFile)
    {
        var photoUrl = await photoStorage.UploadPhotoAsync(requestFile);

        await rabbitMqPublisher.PublishPhotoUploadedAsync(new PhotoUploadedMessage());

        return photoUrl;
    }
}