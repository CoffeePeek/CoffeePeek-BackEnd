using CoffeePeek.Infrastructure.Cache;
using CoffeePeek.Infrastructure.Cache.Interfaces;
using CoffeePeek.Infrastructure.Services;
using CoffeePeek.Infrastructure.Services.Auth;
using CoffeePeek.Infrastructure.Services.Auth.Interfaces;
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
        
        return services;
    }
}