
namespace Hangfire.Business.Settings
{
    public class QueueSettings
    {
        public string SchedulerQueue { get; set; } = string.Empty;
        public string NotifyQueue { get; set; } = string.Empty;
        public int MaxParallelsHandler { get; set; }
    }
}