using CoffeePeek.Photo.Application.Interfaces;
using CoffeePeek.Photo.Infrastructure.Options;
using CoffeePeek.Photo.Infrastructure.Storage;

namespace CoffeePeek.Photo.Api.Configuration;

internal static class InfrastructureModule
{
    public static IServiceCollection LoadInfrastructure(this IServiceCollection services)
    {
        services.AddValidateOptions<DigitalOceanOptions>();

        services.AddScoped<IPhotoStorage, S3PhotoStorage>();

        return services;
    }
}