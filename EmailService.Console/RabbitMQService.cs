using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace EmailService.Console
{
    public class RabbitMQService
    {
        public static void ReceiveFromQueueContinuously()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "email.exchange", type: ExchangeType.Fanout);

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                              exchange: "email.exchange",
                              routingKey: "");

            System.Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var productObj = JObject.Parse(message);
                var productId = productObj["Id"].ToString();
                var productName = productObj["Name"].ToString();

                System.Console.WriteLine("[x] received productId:{0}, productName:{1}", productId, productName);

                string emailBody = string.Format("productId:{0}, productName:{1} has been added ..", productId, productName);
                EmailNotificationService.SendEmail(emailBody);
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            while (true)
            {
                Thread.Sleep(3 * 1000);
            }
        }
    }
}
