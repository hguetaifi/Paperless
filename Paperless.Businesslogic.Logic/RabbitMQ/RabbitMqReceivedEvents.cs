using Paperless.Businesslogic.Interfaces;

namespace Paperless.Businesslogic.Logic.RabbitMQ
{
    public class RabbitMqReceivedEvents : IRabbitMqReceivedEvents
    {
        public RabbitMqReceivedEvents(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}