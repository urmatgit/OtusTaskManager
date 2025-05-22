namespace RabbitMq.Connector.Consumer.EventConsumer
{
    /// <summary>
    /// Опции регистрируемого обработчика
    /// </summary>
    public class HandlerOption
    {
        /// <summary>
        /// Объект-тип обработчика события от AMPQ
        /// </summary>
        public Type SubscribeHandlerType { get; set; } = default!;

        /// <summary>
        /// Удалить очередь из сообщения сразу , либо только после завершения обработки сообщения
        /// </summary>
        public bool AutoAck { get; set; }

        /// <summary>
        /// Максимальное количество обрабатываемых сообщений обработчиком
        /// BasicQos(0, MaxPrefetchCount, false)
        /// </summary>
        public ushort MaxPrefetchCount { get; set; }

        /// <summary>
        /// Название точки обмена
        /// </summary>
        public string ExchangeName { get; set; } = default!;

        /// <summary>
        /// Название очереди
        /// </summary>
        public string QueueName { get; set; } = default!;

        /// <summary>
        /// Ключ роутинга, обычно совпадает с названием очереди
        /// </summary>
        public string RoutingKey { get; set; } = default!;
    }
}