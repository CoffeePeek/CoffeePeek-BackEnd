using System.Reflection;
using CoffeePeek.Api.Middleware;
using CoffeePeek.BuildingBlocks.AuthOptions;
using CoffeePeek.BuildingBlocks.EfCore;
using CoffeePeek.BuildingBlocks.Extensions;
using CoffeePeek.BuildingBlocks.Options;
using CoffeePeek.BuildingBlocks.Sentry;
using CoffeePeek.BuildingBlocks.Services;
using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Mapper;
using CoffeePeek.Data.Models.Users;
using Mapster;
using Microsoft.AspNetCore.Identity;
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
    .AddDbContext<ReviewCoffeePeekDbContext>(opt =>
    {
        opt.UseNpgsql(dbOptions.ReviewConnectionString, b => b.MigrationsAssembly("CoffeePeek.Data"));
    })
    .ConfigureDbRepositories();

builder.Services.AddMapster();
MapsterConfig.MapperConfigure();

builder.Services
    .AddSwagger()
    .AddBearerAuthentication()
    .AddUserIdentity()
    .AddBusinessServices()
    .AddControllers();

var app = builder.Build();

/*using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRoleEntity>>();
    
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRoleEntity("Admin"));
        await roleManager.CreateAsync(new IdentityRoleEntity("Merchant"));
        await roleManager.CreateAsync(new IdentityRoleEntity("User"));
    }
}*/


app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<UserTokenMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

