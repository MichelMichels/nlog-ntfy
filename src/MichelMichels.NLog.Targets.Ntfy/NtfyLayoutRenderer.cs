using NLog;
using NLog.LayoutRenderers;
using System.Text;

namespace MichelMichels.NLog.Targets.Ntfy;

[LayoutRenderer("ntfy")]
public sealed class NtfyLayoutRenderer : LayoutRenderer
{
    public bool IsDateRendered { get; set; } = true;

    protected override void Append(StringBuilder builder, LogEventInfo logEvent)
    {
        RenderDate(builder, logEvent);
        RenderMessage(builder, logEvent);
        RenderException(builder, logEvent);
        RenderStackTrace(builder, logEvent);
    }

    private void RenderDate(StringBuilder builder, LogEventInfo logEvent)
    {
        if (IsDateRendered)
        {
            builder.AppendLine($"Date: {logEvent.TimeStamp:dd/MM/yyyy}");
            builder.AppendLine($"Time: {logEvent.TimeStamp:HH:mm}");
            builder.AppendLine();
        }
    }
    private void RenderMessage(StringBuilder builder, LogEventInfo logEvent)
    {
        builder.AppendLine($"{logEvent.Message}");
    }
    private void RenderException(StringBuilder builder, LogEventInfo logEvent)
    {
        if (logEvent.Exception != null)
        {
            builder.AppendLine();
            builder.AppendLine($"Exception: {logEvent.Exception.ToString()}");
        }
    }
    private void RenderStackTrace(StringBuilder builder, LogEventInfo logEvent)
    {
        if (logEvent.HasStackTrace)
        {
            builder.AppendLine();
            builder.AppendLine($"StackTrace: {logEvent.StackTrace.ToString()}");
        }
    }
}
