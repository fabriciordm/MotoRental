using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MotoRental.Producer.Producers
{
    public class RabbitMqService
    {
        private readonly string _hostName;
        private readonly string _queueName;

        //public RabbitMqService(string hostName, string queueName)
        //{
        //    _hostName = hostName;
        //    _queueName = queueName;
        //}

        //public void SendMessage(string message)
        //{
        //    var factory = new ConnectionFactory() { HostName = _hostName };
        //    using (var connection = factory.CreateConnection())
        //    using (var channel = connection.CreateModel())
        //    {
        //        channel.QueueDeclare(queue: _queueName,
        //                             durable: false,
        //                             exclusive: false,
        //                             autoDelete: false,
        //                             arguments: null);

        //        var body = Encoding.UTF8.GetBytes(message);

        //        channel.BasicPublish(exchange: "",
        //                             routingKey: _queueName,
        //                             basicProperties: null,
        //                             body: body);
        //    }
        //}

        public RabbitMqService(string hostName, string queueName)
        {
            _hostName = hostName;
            _queueName = queueName;
        }

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "motorental-exchange",
                                     routingKey: _queueName,
                                     basicProperties: null,
                                     body: body);
            }
        }

       

        
    }
}
