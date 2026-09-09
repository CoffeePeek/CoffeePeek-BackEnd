using CoffeePeek.AccountService;
using JasperFx.CodeGeneration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wolverine;
using Wolverine.Runtime;

namespace CoffeePeek.Account.Infrastructure.Tests;

public class WolverinePreGeneratedHandlersTests
{
    [Fact]
    public async Task ProductionAccountHost_HasEveryRequiredPreGeneratedHandler()
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            EnvironmentName = Environments.Production,
            ApplicationName = typeof(InfrastructureExtensions).Assembly.FullName
        });
        builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["Sentry:Dsn"] = "",
            ["PostgresCpOptions:ConnectionString"] = "Host=localhost;Database=codegen_test;Username=test;Password=test",
            ["RabbitMqOptions:HostName"] = "localhost",
            ["RabbitMqOptions:Port"] = "5672",
            ["RabbitMqOptions:Username"] = "guest",
            ["RabbitMqOptions:Password"] = "guest",
            ["RedisOptions:Host"] = "localhost",
            ["RedisOptions:Port"] = "6379",
            ["JWTOptions:SecretKey"] = "codegen-test-secret-at-least-32-characters",
            ["JWTOptions:Issuer"] = "CoffeePeek.WEB",
            ["JWTOptions:Audience"] = "CoffeePeek.API",
            ["JWTOptions:AccessTokenLifetimeMinutes"] = "60",
            ["JWTOptions:RefreshTokenLifetimeDays"] = "7",
            ["GatewayAuth:SecretKey"] = "codegen-test-gateway-secret-at-least-32-characters",
            ["MinIOOptions:Endpoint"] = "http://localhost:9000",
            ["MinIOOptions:AccessKey"] = "test",
            ["MinIOOptions:SecretKey"] = "test",
            ["MediaPublicUrlOptions:PublicEndpoint"] = "http://localhost:9000",
            ["MediaPublicUrlOptions:ShopBucketName"] = "coffeepeek.shops",
            ["MediaPublicUrlOptions:AvatarBucketName"] = "coffeepeek.avatars",
            ["ResendClientOptions:ApiToken"] = "re_test_placeholder"
        });
        builder.AddApplication();

        // Build the real production handler graph without starting external transports.
        await using var app = builder.Build();
        var options = app.Services.GetRequiredService<WolverineOptions>();
        Assert.Equal(TypeLoadMode.Static, options.CodeGeneration.TypeLoadMode);
        var runtime = (WolverineRuntime)app.Services.GetRequiredService<IWolverineRuntime>();
        runtime.Handlers.AssertPreBuiltTypesExist(options);
    }
}
