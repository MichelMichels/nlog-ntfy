using Microsoft.Extensions.Logging;

namespace Demo
{
    public class Runner
    {
        private readonly ILogger<Runner> logger;

        public Runner(ILogger<Runner> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Run(string name)
        {
            logger.LogTrace("This is a trace message.");
            Thread.Sleep(1000);

            logger.LogDebug("This is a debug message.");
            Thread.Sleep(1000);

            logger.LogInformation("This is an information message.");
            Thread.Sleep(1000);

            logger.LogWarning("This is a warning message.");
            Thread.Sleep(1000);

            logger.LogError(new ArgumentNullException("Test exception"), "This is an error message.");
            Thread.Sleep(1000);

            logger.LogCritical(new ArgumentNullException("Test exception"), "This is a critical message.");
        }
    }
}
