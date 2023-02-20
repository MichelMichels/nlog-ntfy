using NLog;
using NLog.Config;
using NLog.Targets;

namespace MichelMichels.NLog.Targets.Ntfy
{
    [Target("Ntfy")]
    public sealed class NtfyTarget : AsyncTaskTarget
    {
        private HttpClient? httpClient;

        public NtfyTarget()
        {
            Topic = "nlog-ntfy";
            Title = "NLog";
            Host = @"https://ntfy.sh/";
        }

        [RequiredParameter]
        public string Topic { get; set; }

        [RequiredParameter]
        public string Title { get; set; }

        [RequiredParameter]
        public string Host { get; set; }

        public string TraceTags { get; set; } = "";
        public string DebugTags { get; set; } = "computer";
        public string InformationTags { get; set; } = "information_source";
        public string WarnTags { get; set; } = "warning";
        public string ErrorTags { get; set; } = "exclamation";
        public string FatalTags { get; set; } = "skull";
        public string DefaultTags { get; set; } = "";

        protected override void InitializeTarget()
        {
            base.InitializeTarget();

            httpClient = new HttpClient()
            {
                BaseAddress = new Uri(Host),
            };
        }

        protected override void CloseTarget()
        {
            httpClient?.Dispose();
            base.CloseTarget();
        }

        protected override Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken token)
        {           
            return SendTheMessageToRemoteHost(logEvent);
        }

        private async Task SendTheMessageToRemoteHost(LogEventInfo logEvent) 
        {
            if(httpClient == null)
            {
                return;
            }

            var request = new HttpRequestMessage(HttpMethod.Post, Topic);
            request.Headers.Add(NtfyHeaders.Title, $"{logEvent.Level.Name} - {Title}");
            request.Headers.Add(NtfyHeaders.Tags, GetTags(logEvent.Level));
            request.Content = new StringContent(RenderLogEvent(this.Layout, logEvent));

            await httpClient.SendAsync(request);
        }

        private string GetTags(LogLevel logLevel)
        {            
            return logLevel.Name switch
            {
                nameof(LogLevel.Trace) => TraceTags,
                nameof(LogLevel.Debug) => DebugTags,
                nameof(LogLevel.Info) => InformationTags,
                nameof(LogLevel.Warn) => WarnTags,
                nameof(LogLevel.Error) => ErrorTags,
                nameof(LogLevel.Fatal) => FatalTags,
                _ => DefaultTags,
            };
        }
    }
}
