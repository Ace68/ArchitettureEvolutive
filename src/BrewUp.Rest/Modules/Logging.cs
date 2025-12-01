using Serilog;
using Serilog.Core;

namespace BrewUp.Rest.Modules;

internal static class Logging
{
    internal static Logger Build(ConfigurationManager configuration) =>
        new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
}