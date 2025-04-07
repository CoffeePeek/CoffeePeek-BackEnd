using System.Text;
using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Entities.Users;
using CoffeePeek.Data.Models.Users;
using CoffeePeek.Shared.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CoffeePeek.BuildingBlocks.AuthOptions;

public static class AuthBuilderExtensions
{
    public static IServiceCollection AddBearerAuthentication(this IServiceCollection services)
    {
        var authOptions = services.AddValidateOptions<AuthenticationOptions>();
        
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.UseSecurityTokenValidators = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authOptions.JwtSecretKey)),
                    ValidIssuer = authOptions.ValidIssuer,
                    ValidAudience = authOptions.ValidAudience,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
                
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        logger.LogInformation($"Token received: {context.Token}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        logger.LogInformation("Token validated successfully");
            
                        var claims = context.Principal?.Claims;
                        if (claims != null)
                        {
                            foreach (var claim in claims)
                            {
                                logger.LogInformation($"Validated Claim: {claim.Type} - {claim.Value}");
                            }
                        }

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        logger.LogError($"Authentication failed: {context.Exception.Message}");
                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            logger.LogError("Token expired");
                        }
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        logger.LogWarning($"Challenge requested: {context.Error}, {context.ErrorDescription}");
                        return Task.CompletedTask;
                    }

                };

            });
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole(RoleConsts.Admin));
            options.AddPolicy("Merchant", policy => policy.RequireRole(RoleConsts.Merchant));
            options.AddPolicy("User", policy => policy.RequireRole(RoleConsts.User));
        });
        
        return services;
    }

    public static IServiceCollection AddUserIdentity(this IServiceCollection services)
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
        
        return services;
    }
}