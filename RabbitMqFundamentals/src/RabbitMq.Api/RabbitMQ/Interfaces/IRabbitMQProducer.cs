namespace RabbitMq.Api.RabbitMQ.Interfaces
{
    public interface IRabbitMQProducer
    {
        void SendProductMessage<T>(T message);
    }
}
