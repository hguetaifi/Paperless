using Microsoft.Extensions.Configuration;
using Paperless.Businesslogic.Interfaces;
using EasyNetQ;

namespace Paperless.Businesslogic.Logic.RabbitMQ
{
    public class RabbitMqReceiver: IRabbitMqReceiver
    {
        private readonly IConfiguration Configuration;

        public RabbitMqReceiver(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public event EventHandler<IRabbitMqReceivedEvents> OnReceived;

        public void RabbitMqReceiveMessage()
        {
            var settings = new
            {
                Host = Configuration["RabbitMQ:Host"],
                UserName = Configuration["RabbitMQ:UserName"],
                Password = Configuration["RabbitMQ:Password"],
                Queue = Configuration["RabbitMQ:Queue"]
            };

            string connectionInfo = $"host={settings.Host};username={settings.UserName};password={settings.Password}";
            var messageBus = RabbitHutch.CreateBus(connectionInfo);

            messageBus.SendReceive.Receive<string>(settings.Queue, HandleMessage);
        }

        private void HandleMessage(string message)
        {
            var queueEvent = new RabbitMqReceivedEvents(message);
            OnMessageReceived(queueEvent);
        }

        protected virtual void OnMessageReceived(IRabbitMqReceivedEvents e)
        {
            OnReceived?.Invoke(this, e);
        }
    }
}