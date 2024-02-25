using EasyNetQ;
using Microsoft.Extensions.Configuration; 


using Paperless.Businesslogic.Interfaces;


namespace Paperless.Businesslogic.Logic.RabbitMQ
{
    public class RabbitMqSender : IRabbitMqSender
    {
        private readonly IConfiguration configuration;

        public RabbitMqSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void RabbitMqSendMessage(string messageContent)
        {
            var rabbitMqSettings = new
            {
                Host = configuration["RabbitMQ:Host"],
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"],
                QueueName = configuration["RabbitMQ:Queue"]
            };

            string connectionString = $"host={rabbitMqSettings.Host};username={rabbitMqSettings.UserName};password={rabbitMqSettings.Password}";

            using (var messageBus = RabbitHutch.CreateBus(connectionString))
            {
                messageBus.SendReceive.Send(rabbitMqSettings.QueueName, messageContent);
            }
        }
    }
}