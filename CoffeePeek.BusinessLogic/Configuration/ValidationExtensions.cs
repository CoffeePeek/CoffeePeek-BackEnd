using CoffeePeek.BusinessLogic.Abstractions;
using CoffeePeek.Contract.Dtos.User;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeePeek.BusinessLogic.Configuration;

public static class ValidationExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidationStrategy<UserDto>, UserCreateValidationStrategy>();
        
        return services;
    }
}