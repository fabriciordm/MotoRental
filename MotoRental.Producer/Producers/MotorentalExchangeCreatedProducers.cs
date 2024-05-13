using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Motorcycle.Domain.Commands.Motorcycle;
using Motorcycle.Domain.Core.Bus;
using MotoRental.CrossCutting.MessageBus;
using MotoRental.CrossCutting.MessageBus.Rabbitmq;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace MotoRental.Producer.Producers
{
    public class MotorentalExchangeCreatedProducers : Publish<MotorentalExchangeCreatedProducers>
    {

        private readonly string _hostName;
        private readonly string _queueName;
        string message = "teste";

        public MotorentalExchangeCreatedProducers()
        {
            
        }

        public void Publish<T>(Publish<T> pub) where T : IntegrationEvent
        {
            SendMessage(pub);
        }

        public void SendMessage(object message)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var jsonMessage = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(jsonMessage);

                channel.BasicPublish(exchange: "",
                                     routingKey: _queueName,
                                     basicProperties: null,
                                     body: body);
            }


        }
    }
}
