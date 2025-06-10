
using Newtonsoft.Json;

namespace Hangfire.Business.Rabbit
{
    public class RegisterMessage
    {
        /// <summary>
        /// Идентификатор пользователя, для которого регистрируется задача.
        /// </summary>
        [JsonProperty("userId")]
        public Guid UserId { get; set; }
        /// <summary>
        /// Дата и время уведомления пользователя.
        /// </summary>
        [JsonProperty("notifyDate")]
        public DateTime NotifyDate { get; set; }
        /// <summary>
        /// Тип задачи, регистрируемой для пользователя.
        /// </summary>
        [JsonProperty("messageType")]
        public RegisterMessageType MessageType { get; set; }
    }

    public enum RegisterMessageType : int 
    {
        TaskExpired
    }
}