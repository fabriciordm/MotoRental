
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MotoRental.CrossCutting.MessageBus.Rabbitmq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MotoRental.Consumer
{
    public class MotorentalExchangeCreatedSubscriber :  RabbitListener
    {
        private readonly ILogger<RabbitListener> _logger;
        private readonly IServiceProvider _services;
        public MotorentalExchangeCreatedSubscriber(IServiceProvider services, IOptions<RabbitSettings> options, ILogger<RabbitListener> logger) : base(options)
        {
            base.ExchangeName = "motorental-exchange";
            base.RouteKey = "motorental-queue";

            base.QueueName = "motorental-queue";
            base.TypeName = "direct"; //topic


            _logger = logger;
            _services = services;
        }

        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //var consumer = new EventingBasicConsumer(_channel);
            //consumer.Received += (sender, args) =>
            //{
            //    var array= EventArgs.Body.ToArray();
            //    var contentString = Encoding.UTF8.GetString(array);
            //    var message =JsonSerializer.Deserialize<>
            //}
            throw new NotImplementedException();
        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
