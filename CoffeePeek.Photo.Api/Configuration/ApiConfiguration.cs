namespace CoffeePeek.Photo.Api.Configuration;

public static class ApiConfiguration
{
    public static IServiceCollection ConfigureApi(this IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        
        services.AddSingleton(config);

        services.LoadInfrastructure();
        services.LoadApplication();

        
        return services;
    }
}