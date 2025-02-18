using System.Text;
using CoffeePeek.BuildingBlocks.Options;
using CoffeePeek.BusinessLogic.Services.Auth;
using CoffeePeek.Contract.Constants;
using CoffeePeek.Contract.Options;
using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Models.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CoffeePeek.BuildingBlocks.AuthOptions;

public static class AuthBuilderExtensions
{
    public static IServiceCollection AddBearerAuthentication(this IServiceCollection services)
    {
        var authOptions = services.AddValidateOptions<AuthenticationOptions>();
        
        services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.UseSecurityTokenValidators = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authOptions.JwtSecretKey)),
                    ValidIssuer = "test",
                    ValidAudience = "test",
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole(RoleConsts.Admin));
            options.AddPolicy("Merchant", policy => policy.RequireRole(RoleConsts.Merchant));
            options.AddPolicy("User", policy => policy.RequireRole(RoleConsts.User));
        });
        
        AddUserIdentity(services);

        services.AddTransient<IAuthService, AuthService>();
        return services;
    }

    private static void AddUserIdentity(IServiceCollection services)
    {
        services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.User.AllowedUserNameCharacters = null;
            })
            .AddEntityFrameworkStores<CoffeePeekDbContext>()
            .AddUserManager<UserManager<User>>()
            .AddUserStore<UserStore<User, IdentityRoleEntity, CoffeePeekDbContext, int>>();
    }
}