using System.Net;
using System.Net.Sockets;
using CoffeePeek.Gateway.Infrastructure;
using FluentAssertions;

namespace CoffeePeek.Gateway.Tests.Infrastructure;

public sealed class ResilientForwarderHttpClientFactoryTests
{
    [Fact]
    public async Task GetRequest_WhenConnectionIsTemporarilyRefused_RetriesAndReturnsResponse()
    {
        var transport = new StubTransport(failuresBeforeSuccess: 2);
        using var handler = ResilientForwarderHttpClientFactory.CreateHandler(
            transport,
            TimeSpan.Zero);
        using var client = new HttpMessageInvoker(handler);

        using var response = await client.SendAsync(
            new HttpRequestMessage(HttpMethod.Get, "http://shops/api/Catalogs/equipments"),
            CancellationToken.None);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        transport.Attempts.Should().Be(3);
    }

    [Fact]
    public async Task PostRequest_WhenConnectionIsRefused_DoesNotRetry()
    {
        var transport = new StubTransport(failuresBeforeSuccess: 1);
        using var handler = ResilientForwarderHttpClientFactory.CreateHandler(
            transport,
            TimeSpan.Zero);
        using var client = new HttpMessageInvoker(handler);

        var act = () => client.SendAsync(
            new HttpRequestMessage(HttpMethod.Post, "http://shops/api/Catalogs"),
            CancellationToken.None);

        await act.Should().ThrowAsync<HttpRequestException>();
        transport.Attempts.Should().Be(1);
    }

    private sealed class StubTransport(int failuresBeforeSuccess) : HttpMessageHandler
    {
        public int Attempts { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Attempts++;

            if (Attempts <= failuresBeforeSuccess)
            {
                return Task.FromException<HttpResponseMessage>(new HttpRequestException(
                    "Connection refused (shops:8080)",
                    new SocketException((int)SocketError.ConnectionRefused)));
            }

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }
}
