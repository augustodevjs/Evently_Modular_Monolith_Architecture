using Serilog;

namespace Evently.API.Extensions;

internal static class LoggingExtensions
{
    internal static void ConfigureApplicationLogging(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context,
            logger) => logger.ReadFrom.Configuration(context.Configuration));
    }

    internal static void ConfigureRequestLogging(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging(o =>
        {
            o.IncludeQueryInRequestPath = true;
        });
    }
}
