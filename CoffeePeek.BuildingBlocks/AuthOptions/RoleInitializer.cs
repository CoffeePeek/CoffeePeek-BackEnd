using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoffeePeek.BuildingBlocks.AuthOptions;

public class RoleInitializer(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await EnsureRoleExistsAsync(roleManager, RoleConsts.Admin);
        await EnsureRoleExistsAsync(roleManager, RoleConsts.Merchant);
        await EnsureRoleExistsAsync(roleManager, RoleConsts.User);
    }   

    private async Task EnsureRoleExistsAsync(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}