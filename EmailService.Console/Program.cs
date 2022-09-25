using System;


namespace EmailService.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.Console.WriteLine("Hello World!");
            //EmailNotificationService.SendEmail();
            RabbitMQService.ReceiveFromQueueContinuously();
        }
    }
}
