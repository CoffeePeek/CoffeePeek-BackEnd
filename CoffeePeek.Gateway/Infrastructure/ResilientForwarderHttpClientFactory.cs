using System.Net.Sockets;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using Polly.Retry;
using Yarp.ReverseProxy.Forwarder;

namespace CoffeePeek.Gateway.Infrastructure;

/// <summary>
/// Adds narrowly scoped transport retries to YARP's own HTTP transport.
/// </summary>
internal sealed class ResilientForwarderHttpClientFactory : ForwarderHttpClientFactory
{
    private static readonly TimeSpan DefaultRetryDelay = TimeSpan.FromMilliseconds(500);

    protected override HttpMessageHandler WrapHandler(
        ForwarderHttpClientContext context,
        HttpMessageHandler handler)
    {
        var innerHandler = base.WrapHandler(context, handler);
        return CreateHandler(innerHandler, DefaultRetryDelay);
    }

    internal static HttpMessageHandler CreateHandler(
        HttpMessageHandler innerHandler,
        TimeSpan retryDelay)
    {
        ArgumentNullException.ThrowIfNull(innerHandler);

        var retryPipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
            .AddRetry(new RetryStrategyOptions<HttpResponseMessage>
            {
                ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .Handle<HttpRequestException>(IsConnectionRefused),
                MaxRetryAttempts = 3,
                Delay = retryDelay,
                BackoffType = DelayBackoffType.Exponential,
                UseJitter = retryDelay > TimeSpan.Zero
            })
            .Build();

        var passThroughPipeline = new ResiliencePipelineBuilder<HttpResponseMessage>().Build();

        return new ResilienceHandler(request => IsRetryableMethod(request.Method)
                ? retryPipeline
                : passThroughPipeline)
        {
            InnerHandler = innerHandler
        };
    }

    private static bool IsRetryableMethod(HttpMethod method) =>
        method == HttpMethod.Get ||
        method == HttpMethod.Head ||
        method == HttpMethod.Options ||
        method == HttpMethod.Trace;

    private static bool IsConnectionRefused(HttpRequestException exception)
    {
        for (Exception? current = exception; current is not null; current = current.InnerException)
        {
            if (current is SocketException { SocketErrorCode: SocketError.ConnectionRefused })
            {
                return true;
            }
        }

        return false;
    }
}
