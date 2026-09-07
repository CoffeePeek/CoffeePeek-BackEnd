using System.Text.Json;
using FluentAssertions;

namespace CoffeePeek.Gateway.Tests;

public class ReverseProxyRouteConfigurationTests
{
    [Theory]
    [InlineData("shops-admin-cities-route", "/api/admin/cities/{**remainder}")]
    [InlineData("shops-admin-equipments-route", "/api/admin/equipments/{**remainder}")]
    [InlineData("shops-admin-beans-route", "/api/admin/beans/{**remainder}")]
    [InlineData("shops-admin-roasters-route", "/api/admin/roasters/{**remainder}")]
    [InlineData("shops-admin-brew-methods-route", "/api/admin/brew-methods/{**remainder}")]
    public void AdminCatalogCrudRoutes_TargetShopsService(string routeId, string path)
    {
        using var document = LoadGatewayAppsettings();
        var routes = document.RootElement.GetProperty("ReverseProxy").GetProperty("Routes");

        routes.TryGetProperty(routeId, out var route).Should().BeTrue();
        route.GetProperty("ClusterId").GetString().Should().Be("shops-cluster");
        route.GetProperty("AuthorizationPolicy").GetString().Should().Be("Moderator");
        route.GetProperty("Match").GetProperty("Path").GetString().Should().Be(path);
    }

    [Fact]
    public void AccountAdminCatchAll_RemainsAfterShopsAdminCatalogRoutes()
    {
        using var document = LoadGatewayAppsettings();
        var routeIds = document.RootElement
            .GetProperty("ReverseProxy")
            .GetProperty("Routes")
            .EnumerateObject()
            .Select(route => route.Name)
            .ToList();

        var accountAdminIndex = routeIds.IndexOf("account-admin-route");
        accountAdminIndex.Should().BeGreaterThan(-1);

        foreach (var shopsCatalogRoute in new[]
        {
            "shops-admin-cities-route",
            "shops-admin-equipments-route",
            "shops-admin-beans-route",
            "shops-admin-roasters-route",
            "shops-admin-brew-methods-route"
        })
        {
            routeIds.IndexOf(shopsCatalogRoute).Should().BeLessThan(accountAdminIndex);
        }
    }

    private static JsonDocument LoadGatewayAppsettings()
    {
        var path = Path.GetFullPath(Path.Combine(
            AppContext.BaseDirectory,
            "..",
            "..",
            "..",
            "..",
            "CoffeePeek.Gateway",
            "appsettings.json"));

        return JsonDocument.Parse(File.ReadAllText(path));
    }
}
