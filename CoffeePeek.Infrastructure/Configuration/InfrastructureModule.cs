using CoffeePeek.BuildingBlocks;
using CoffeePeek.Infrastructure.Cache;
using CoffeePeek.Infrastructure.Cache.Interfaces;
using CoffeePeek.Infrastructure.Consumers;
using CoffeePeek.Infrastructure.Services;
using CoffeePeek.Infrastructure.Services.Auth;
using CoffeePeek.Infrastructure.Services.Auth.Interfaces;
using CoffeePeek.Shared.Extensions.Configuration;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeePeek.Infrastructure.Configuration;

public static class InfrastructureModule
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        #region Auth
        
        services.AddTransient<IAuthService, AuthService>();
        services.AddScoped<IHashingService, HashingService>();

        #endregion
        
        #region Infrastructure

        services.AddTransient<IRedisService, RedisService>();
        services.AddTransient<ICacheService, CacheService>();
        
        #endregion
        
        #region MassTransit

        services.AddValidateOptions<RabbitMqOptions>();
        
        var rabbitMqOptions = services.GetOptions<RabbitMqOptions>();
        
        services.AddMassTransit(x =>
        {
            x.AddConsumer<PhotoUploadResultConsumer>();

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
        
        #endregion
        
        return services;
    }
}