using Newtonsoft.Json.Linq;

namespace RabbitMq.Connector.Publisher.EventPublisher;

public class MqPersistentModel
{
    public string? EventType { get; }
    public string? EntityType { get; }
    public string RoutingKey => $"{EventType}.{EntityType}";
    public object Obj { get; }
    public JObject JObject { get; set; } = default!;

    public MqPersistentModel(string eventType, string entityType, object obj)
    {
        EventType = eventType ?? throw new ArgumentNullException(nameof(eventType));
        EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
        Obj = obj ?? throw new ArgumentNullException(nameof(obj));
    }

    public MqPersistentModel(object obj)
    {
        Obj = obj;
    }
}