using CatalogService.Core.Interfaces.Services;
using RabbitMQ.Client;
using System.Text;

namespace CatalogService.Service.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        public void SendToQueue(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "email.exchange", type: ExchangeType.Fanout);

                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "email.exchange",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                System.Console.WriteLine(" [x] Sent {0}", message);
            }
        }       
    }
}
