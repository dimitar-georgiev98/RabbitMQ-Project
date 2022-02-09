using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQ.Consumer
{
    class Consumer
    {
        static void Main(string[] args)
        {
            var connect = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = connect.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                Console.WriteLine("Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;
                    Console.WriteLine("Received '{0}':'{1}'",
                        routingKey,
                        message);
                };
                channel.BasicConsume(
                    queue: "message",
                    autoAck: true,
                    consumer: consumer);

                Console.ReadLine();
            }
        }
    }
}
