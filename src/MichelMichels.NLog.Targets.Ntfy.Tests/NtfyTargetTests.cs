namespace MichelMichels.NLog.Targets.Ntfy.Tests;

[TestClass]
public class NtfyTargetTests
{
    [TestMethod]
    public void DefaultConstructorTest()
    {
        // Arrange
        var target = new NtfyTarget();

        // Act

        // Assert
        Assert.AreEqual("nlog-ntfy", target.Topic);
        Assert.AreEqual("NLog", target.Title);
        Assert.AreEqual(@"https://ntfy.sh/", target.Host);
    }

    [TestMethod]
    public void DefaultTagsTest()
    {
        // Arrange
        var target = new NtfyTarget();

        // Act

        // Assert
        Assert.AreEqual("", target.DefaultTags);
        Assert.AreEqual("", target.TraceTags);
        Assert.AreEqual("computer", target.DebugTags);
        Assert.AreEqual("information_source", target.InformationTags);
        Assert.AreEqual("warning", target.WarnTags);
        Assert.AreEqual("exclamation", target.ErrorTags);
        Assert.AreEqual("skull", target.FatalTags);

    }
}
