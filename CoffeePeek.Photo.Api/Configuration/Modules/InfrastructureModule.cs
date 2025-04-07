using CoffeePeek.Photo.Core.Interfaces;
using CoffeePeek.Photo.Infrastructure.Consumers;
using CoffeePeek.Photo.Infrastructure.Options;
using CoffeePeek.Photo.Infrastructure.Storage;
using CoffeePeek.Shared.Extensions.Configuration;
using MassTransit;

namespace CoffeePeek.Photo.Api.Configuration;

internal static class InfrastructureModule
{
    public static IServiceCollection LoadInfrastructure(this IServiceCollection services)
    {
        services.AddValidateOptions<DigitalOceanOptions>();
        
        services.AddScoped<IPhotoStorage, S3PhotoStorage>();
        
        ConfigureMassTransit(services);

        return services;
    }

    private static void ConfigureMassTransit(IServiceCollection services)
    {
        services.AddValidateOptions<RabbitMqOptions>();
        
        var rabbitMqOptions = services.GetOptions<RabbitMqOptions>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<PhotoUploadRequestedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqOptions.HostName, rabbitMqOptions.Port, "/", h =>
                {
                    h.Username(rabbitMqOptions.Username);
                    h.Password(rabbitMqOptions.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}