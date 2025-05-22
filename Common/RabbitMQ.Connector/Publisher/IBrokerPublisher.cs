namespace RabbitMq.Connector.Publisher;

public interface IBrokerPublisher<in T> where T : class
{
    void Publish(T entity);
}