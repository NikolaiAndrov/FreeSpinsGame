using Microsoft.Extensions.Logging;

namespace FreeSpinsGame.Services.Tests
{
    internal static class LoggingFactoryCreator
    {
        internal static ILoggerFactory CreateSimpleLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
            {
                builder.AddConsole();

                builder.SetMinimumLevel(LogLevel.Information);

                builder.AddFilter("Microsoft", LogLevel.Warning);
            });
        }
    }
}
