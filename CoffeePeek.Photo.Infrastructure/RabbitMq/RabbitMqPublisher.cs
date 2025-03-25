using System.Text;
using System.Text.Json;
using CoffeePeek.Photo.Application.Interfaces;
using CoffeePeek.Photo.Application.Messaging;
using RabbitMQ.Client;

namespace CoffeePeek.Photo.Infrastructure.RabbitMQ;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly ConnectionFactory _factory;

    public RabbitMqPublisher(string hostName, string queueName)
    {
        _factory = new ConnectionFactory { HostName = hostName };
        QueueName = queueName;
    }

    public string QueueName { get; }

    public async Task PublishPhotoUploadedAsync(PhotoUploadedMessage message)
    {
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        await channel.BasicPublishAsync(exchange: "",
            routingKey: QueueName,
            body: body);

        await Task.CompletedTask;
    }
}