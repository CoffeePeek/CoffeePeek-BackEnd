using CoffeePeek.Photo.Application.Interfaces;
using CoffeePeek.Photo.Application.Services;
using CoffeePeek.Photo.Infrastructure.RabbitMQ;

namespace CoffeePeek.Photo.Api.Configuration;

internal static class ApplicationModule
{
    public static IServiceCollection LoadApplication(this IServiceCollection services)
    {
        services.AddValidateOptions<RabbitMqOptions>();
        
        services.AddScoped<IPhotoUploadService, PhotoUploadService>();
        
        var rabbitMqOptions = services.GetOptions<RabbitMqOptions>();
        services.AddSingleton<IRabbitMqPublisher>(sp =>
            new RabbitMqPublisher(rabbitMqOptions.HostName, rabbitMqOptions.QueueName));
        
        return services;
    }
}