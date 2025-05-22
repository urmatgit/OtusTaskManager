namespace RabbitMq.Connector.Publisher.EventPublisher;

public interface IEventBrokerPublisher
{
    void SendMqPersistentModel(MqPersistentModel model, string exchangeName, string queueName, string exchangeType = "direct");
    public void Send<TObject>(TObject obj, string exchangeName, string queueName, string exchangeType = "direct");
}