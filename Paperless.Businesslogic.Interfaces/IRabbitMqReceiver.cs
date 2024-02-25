namespace Paperless.Businesslogic.Interfaces
{
    public interface IRabbitMqReceiver
    {
    public event EventHandler<IRabbitMqReceivedEvents> OnReceived;
    public void RabbitMqReceiveMessage();
    }
}