using NLog;

namespace MichelMichels.NLog.Targets.Ntfy.Tests
{
    [TestClass]
    public class NtfyLayoutRendererTests
    {
        [TestMethod]
        public void DefaultDateRendered()
        {
            // Arrange
            var layoutRenderer = new NtfyLayoutRenderer();

            // Act

            // Assert            
            Assert.IsTrue(layoutRenderer.IsDateRendered);
        }

        [TestMethod]
        public void DateNotRendered()
        {
            // Arrange
            var layoutRenderer = new NtfyLayoutRenderer()
            {
                IsDateRendered = false,
            };
            var logEvent = new LogEventInfo(LogLevel.Info, "TestLogger", "This is a test message.");

            // Act
            var result = layoutRenderer.Render(logEvent);

            // Assert            
            Assert.IsFalse(layoutRenderer.IsDateRendered);
            Assert.AreEqual(2, result.Split('\n').Length);
        }

        [TestMethod]
        public void DebugRenderTest()
        {
            // Arrange
            var renderer = new NtfyLayoutRenderer();
            var logEvent = new LogEventInfo(LogLevel.Debug, "TestLogger", "This is a test log event.");
            var date = logEvent.TimeStamp;
            var dateString = date.ToString("dd/MM/yyyy");
            var timeString = date.ToString("HH:mm");

            // Act
            var result = renderer.Render(logEvent);

            // Assert
            Assert.AreEqual(5, result.Split('\n').Length);
            Assert.IsTrue(result.Split('\n')[0].Contains(dateString));
            Assert.IsTrue(result.Split('\n')[1].Contains(timeString));
            Assert.IsTrue(result.Split('\n')[3].Contains("This is a test log event."));
            Assert.IsTrue(string.IsNullOrEmpty(result.Split("\n")[4]));
        }

        [TestMethod]
        public void ExceptionTest()
        {
            // Arrange
            var renderer = new NtfyLayoutRenderer();
            var logEvent = new LogEventInfo(LogLevel.Error, "TestLogger", "This is a test log event.")
            {
                Exception = new ArgumentNullException(),
            };

            // Act
            var result = renderer.Render(logEvent);

            // Assert
            Assert.AreEqual(7, result.Split('\n').Length);
        }
    }
}