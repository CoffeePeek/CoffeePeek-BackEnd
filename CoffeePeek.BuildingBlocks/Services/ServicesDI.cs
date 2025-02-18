using CoffeePeek.BusinessLogic.Abstractions;
using CoffeePeek.BusinessLogic.Services;
using CoffeePeek.Contract.Dtos.User;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeePeek.BuildingBlocks.Services;

public static class ServicesDI
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IHashingService, HashingService>();

        #region Validation

        services.AddTransient<IValidationStrategy<UserDto>, UserCreateValidationStrategy>();
        
        #endregion

        return services;
    }
}