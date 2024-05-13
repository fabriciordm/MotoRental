using Motorcycle.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motorcycle.Domain.Core;

namespace MotoRental.CrossCutting.MessageBus.Rabbitmq
{
    public interface IRabbitMQClient
    {
        void Publish<T>(Publish<T> pub) where T : IntegrationEvent;
        
    }
}
