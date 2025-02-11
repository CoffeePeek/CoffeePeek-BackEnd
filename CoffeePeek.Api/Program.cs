using System.Reflection;
using System.Text;
using CoffeePeek.BuildingBlocks.AuthOptions;
using CoffeePeek.BuildingBlocks.EfCore;
using CoffeePeek.BuildingBlocks.Options;
using CoffeePeek.BuildingBlocks.Sentry;
using CoffeePeek.BuildingBlocks.Services;
using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Mapper;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Utilities.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.BuildSentry();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.Load("CoffeePeek.BusinessLogic"));
});

var apiOption = builder.Services.AddValidateOptions<ApiOptions>();
var dbOptions = builder.Services.AddValidateOptions<PostgresCpOptions>();

builder.Services
    .AddDbContext<CoffeePeekDbContext>(opt =>
    {
        opt.UseNpgsql(dbOptions.ConnectionString, b => b.MigrationsAssembly("CoffeePeek.Data"));
    })
    .ConfigureDbRepositories();

builder.Services.AddMapster();
MapsterConfig.MapperConfigure();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiOption.JwtSecretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddBusinessServices();

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

