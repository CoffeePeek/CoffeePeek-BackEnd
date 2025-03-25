using System.Text;
using System.Text.Json;
using CoffeePeek.Contract.Messages.Photos;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CoffeePeek.Infrastructure.RabbitMq;

public class PhotoUploadConsumer : BackgroundService
{
    private readonly string _queueName;
    private readonly string _hostName;

    public PhotoUploadConsumer(string hostName, string queueName)
    {
        _queueName = queueName;
        _hostName = hostName;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory { HostName = _hostName };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: _queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            try
            {
                var photoMessage = JsonSerializer.Deserialize<PhotoUploadedMessage>(message);

                if (photoMessage != null)
                {
                    //someLogic
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing message: {ex.Message}");
            }
        };

        await channel.BasicConsumeAsync(queue: _queueName,
            autoAck: true,
            consumer: consumer);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}