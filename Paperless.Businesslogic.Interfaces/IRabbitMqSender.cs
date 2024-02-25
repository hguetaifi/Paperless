namespace Paperless.Businesslogic.Interfaces
{
    public interface IRabbitMqSender
    {
        public void RabbitMqSendMessage(string message);
    }
}