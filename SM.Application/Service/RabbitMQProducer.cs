using RabbitMQ.Client;
using SM.Domaiin.Entities;
using System.Text;
using Newtonsoft.Json;

namespace SM.Application.Service
{
    public class RabbitMQProducer
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "servico_queue";

        public void SendMessage(ServicoMessageRabbitMQ message)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };

            using var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);

        }
    }
}
