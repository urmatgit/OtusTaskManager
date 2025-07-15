
using Newtonsoft.Json;

namespace RabbitMQ.Messages
{
    public enum MessageType : int 
    {
        UserRegistered,
        UserProjectAdded,
        UserProjectRemoved,
        TaskAdded,
        TaskCompleted,
        TaskExpired
    }

    public class NotifyMessage
    {
        [JsonProperty("messageType")]
        public MessageType MessageType { get; set; }
        [JsonProperty("mailTo")]
        public string MailTo { get; set; } = string.Empty;
        [JsonProperty("displayName")]
        public string DisplayName { get; set; } = string.Empty;
        [JsonProperty("subject")]
        public string Subject { get; set; } = string.Empty;
        [JsonProperty("messageData")]
        public Dictionary<string, string> MessageData { get; set; } = [];
    }
}