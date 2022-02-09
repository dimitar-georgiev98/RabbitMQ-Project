using System;
using System.Linq;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace RabbitMQ.Client
{
    public class RabbitMQClient
    {
        static void Main(string[] args)
        {
            var connect = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = connect.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "direct_logs", type: "direct");
                var routingKey = "message";
                var body = Encoding.UTF8.GetBytes("Hello");

                while (true)
                {
                    channel.BasicPublish(
                        exchange: "direct_logs",
                        routingKey: routingKey,
                        basicProperties: null,
                        body: body);

                    Console.WriteLine("Sent '{0}': Hello", routingKey);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}