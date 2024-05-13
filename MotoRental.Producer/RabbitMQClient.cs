using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Motorcycle.Domain.Core.Bus;
using MotoRental.CrossCutting.MessageBus.Rabbitmq;
using MotoRental.CrossCutting.MessageBus;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MotoRental.Producer
{
    public class RabbitMQClient : IRabbitMQClient
    {
        private IModel _channel;
        private ILogger _logger;
        private readonly RabbitSettings _settings;

        public RabbitMQClient(IOptions<RabbitSettings> options, ILogger<RabbitMQClient> logger)
        {
            _settings = options.Value;
            _logger = logger;
            TryConnect();

        }
        public virtual void PushMessage(string routingKey, object message)
        {
            _logger.LogInformation($"PushMessage,routingKey:{routingKey}");
            _channel.QueueDeclare(queue: "message",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

            string msgJson = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(msgJson);

            _channel.BasicPublish(exchange: "message",
                                    routingKey: routingKey,
                                    basicProperties: null,
                                    body: body);
        }
        
        public void Publish(Producer.Producers.Publish pub) 
        {
            _logger.LogInformation($"PushMessage,routingKey:{pub.RoutingKey}");

            try
            {
                IBasicProperties props = _channel.CreateBasicProperties();
                props.DeliveryMode = 2;
                props.Headers = pub.Headers;

                _channel.ExchangeDeclare(exchange: pub.ExchangeName, type: pub.ExchangeType, durable: true);

                _channel.QueueDeclare(queue: pub.QueueName, durable: true, exclusive: false,
                    autoDelete: false, arguments: pub.Arguments);

                _channel.BasicPublish(exchange: pub.ExchangeName, routingKey: pub.RoutingKey,
                    mandatory: true, basicProperties: props, body: pub.Body);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void TryConnect()
        {
            var policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _settings.Host,
                    UserName = _settings.UserName,
                    Password = _settings.Password,
                    Port = _settings.Port,
                    VirtualHost = _settings.VirtualHost,
                    ClientProvidedName = $"Motorental-sender"
                };

                var connection = factory.CreateConnection();
                _channel = connection.CreateModel();
            });
        }
    }
}
