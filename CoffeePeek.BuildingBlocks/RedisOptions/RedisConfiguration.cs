using System.Net;
using CoffeePeek.Shared.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace CoffeePeek.BuildingBlocks.RedisOptions;

public static class RedisConfiguration
{
    public static void RedisConfigurationOptions(this IServiceCollection services)
    {
        var options = services.AddValidateOptions<RedisOptions>();

        var redisConfig = new ConfigurationOptions
        {
            EndPoints = { new DnsEndPoint(options.Host, options.Port) },
            AbortOnConnectFail = false,
            Ssl = false,
            Password = options.Password,
            User = "default"
        };

        services.AddSingleton<IConnectionMultiplexer>(_ => 
            ConnectionMultiplexer.Connect(redisConfig));
    }
}