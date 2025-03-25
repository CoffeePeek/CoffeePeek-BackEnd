using CoffeePeek.Photo.Application.Messaging;

namespace CoffeePeek.Photo.Application.Interfaces;

public interface IRabbitMqPublisher
{
    Task PublishPhotoUploadedAsync(PhotoUploadedMessage message);
}