
namespace Hangfire.Business.Settings
{
    public class HangfireSettings
    {
        public QueueSettings Queues { get; set; } = default!;
    }
}