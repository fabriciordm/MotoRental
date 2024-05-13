using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Polly;
using Newtonsoft.Json;

namespace MotoRental.CrossCutting.MessageBus.Rabbitmq
{
    public class RabbitListener : IHostedService
    {
        private IConnection connection;
        private IModel channel;
        private readonly RabbitSettings _settings;

        protected string RouteKey;
        protected string QueueName;
        protected string ExchangeName;
        protected string TypeName;

        protected Dictionary<string, object> Arguments;

        public RabbitListener(IOptions<RabbitSettings> options)
        {
            Arguments = new Dictionary<string, object>();
            _settings = options.Value;
            TryConnect();
        }

        // How to process messages
        public virtual async Task<bool> Process(string message)
        {
            return (message.Length > 0 ? true : false);
            
        }

        // Registered consumer monitoring here
        public void Register()
        {
            //Console.WriteLine($"RabbitListener register,routeKey:{RouteKey}");
           
            channel.ExchangeDeclare(exchange: ExchangeName, type: TypeName, durable: true);

            channel.QueueDeclare(queue: QueueName, exclusive: false, durable: true, autoDelete: false, arguments: Arguments);
            channel.QueueBind(queue: QueueName, exchange: ExchangeName, routingKey: RouteKey);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var result = await Process(message);
                Console.WriteLine($"RabbitListener register,routeKey:{message}");
                if (result)
                    channel.BasicAck(ea.DeliveryTag, false);

            };

            channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
        }
        public void DeRegister()
        {
            this.connection.Close();
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
                    ClientProvidedName = $"Motorental-consumer"
                };
               
                
                this.connection = factory.CreateConnection("consumer-motorental-rabbitmq");
                this.channel = connection.CreateModel();
            });
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Register();
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.connection.Close();
            return Task.CompletedTask;
        }
    }
}
