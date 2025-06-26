
using Newtonsoft.Json;

namespace RabbitMQ.Messages
{
    public class HangfireMessage
    {
        /// <summary>
        /// Идентификатор пользователя, для которого надо запланировать задачу.
        /// </summary>
        [JsonProperty("userId")]
        public Guid UserId { get; set; }
        /// <summary>
        /// Дата выполнения задачи.
        /// </summary>
        [JsonProperty("executionDate")]
        public DateTime ExecutionDate { get; set; }
        /// <summary>
        /// Тип сервиса обрабатывающего задачу.        
        /// </summary>
        [JsonProperty("serviceType")]
        public string ServiceType { get; set; } = string.Empty;
    }
}