namespace CoffeePeek.Infrastructure.RabbitMq.Interfaces;

public interface IPhotoUploadConsumer
{
    Task StartListeningAsync(CancellationToken cancellationToken);
}