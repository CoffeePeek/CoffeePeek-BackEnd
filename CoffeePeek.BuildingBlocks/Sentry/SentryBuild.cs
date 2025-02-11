using Microsoft.AspNetCore.Hosting;

namespace CoffeePeek.BuildingBlocks.Sentry;

public static class SentryBuild
{
    public static IWebHostBuilder BuildSentry(this IWebHostBuilder webHost)
    {
        webHost.UseSentry(options =>
        {
            options.Dsn = "https://346962bc604530086867fbe7e6a80340@o4508693925724160.ingest.de.sentry.io/4508693929263184";

            options.TracesSampleRate = 1.0;

#if DEBUG
            options.Debug = true;
#endif
        });

        return webHost;
    }
}