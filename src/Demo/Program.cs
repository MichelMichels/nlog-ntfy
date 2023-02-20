// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Demo;

Console.WriteLine("ntfy.sh test script.");

var logger = LogManager.GetCurrentClassLogger();
try
{
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();

    using var servicesProvider = new ServiceCollection()
        .AddTransient<Runner>()
        .AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            loggingBuilder.AddNLog(config);
        }).BuildServiceProvider();

    var runner = servicesProvider.GetRequiredService<Runner>();
    runner.Run("Test");

    Console.WriteLine("Press ANY key to exit");
    Console.ReadKey();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of excpetion");
    throw;
}
finally
{
    LogManager.Shutdown();
}