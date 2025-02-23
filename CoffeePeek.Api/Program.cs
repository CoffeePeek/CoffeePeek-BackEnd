using System.Reflection;
using CoffeePeek.Api.Middleware;
using CoffeePeek.BuildingBlocks.AuthOptions;
using CoffeePeek.BuildingBlocks.EfCore;
using CoffeePeek.BuildingBlocks.Extensions;
using CoffeePeek.BuildingBlocks.Options;
using CoffeePeek.BuildingBlocks.RedisOptions;
using CoffeePeek.BuildingBlocks.Sentry;
using CoffeePeek.BusinessLogic.Configuration;
using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Mapper;
using CoffeePeek.Infrastructure.Configuration;
using Mapster;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.BuildSentry();

builder.Services.AddEndpointsApiExplorer();

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.Load("CoffeePeek.BusinessLogic"));
});

var dbOptions = builder.Services.AddValidateOptions<PostgresCpOptions>();
builder.Services
    .AddDbContext<CoffeePeekDbContext>(opt =>
    {
        opt.UseNpgsql(dbOptions.ConnectionString, b => b.MigrationsAssembly("CoffeePeek.Data"));
    })
    .ConfigureDbRepositories();
builder.Services.RedisConfigurationOptions();
builder.Services.AddMapster();
MapsterConfig.MapperConfigure();


builder.Services
    .AddSwagger()
    .AddBearerAuthentication()
    .AddValidators()
    .RegisterInfrastructure()
    .AddControllers();

//builder.Services.AddHostedService<RoleInitializer>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

