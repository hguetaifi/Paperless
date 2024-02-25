namespace Paperless.Businesslogic.Interfaces
{
    public interface IRabbitMqReceivedEvents
    {
        string Message { get; }
    }
}