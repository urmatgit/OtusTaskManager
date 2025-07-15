namespace NotifyService.Business.Settings
{
    public class QueueSettings
    {
        public string NotifyQueue { get; set; } = default!;
        public int MaxParallelsHandler { get; set; }
    }
}