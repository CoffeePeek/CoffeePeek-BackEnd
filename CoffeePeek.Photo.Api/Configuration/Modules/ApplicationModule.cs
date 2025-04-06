using CoffeePeek.Photo.Infrastructure.Consumers;
using MassTransit;

namespace CoffeePeek.Photo.Api.Configuration;

internal static class ApplicationModule
{
    public static IServiceCollection LoadApplication(this IServiceCollection services)
    {
        services.AddValidateOptions<RabbitMqOptions>();
        
        var rabbitMqOptions = services.GetOptions<RabbitMqOptions>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<PhotoUploadRequestedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqOptions.HostName, 28315, "/", h =>
                {
                    h.Username(rabbitMqOptions.Username);
                    h.Password(rabbitMqOptions.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
}