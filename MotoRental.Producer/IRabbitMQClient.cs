using Motorcycle.Domain.Core.Bus;
using MotoRental.CrossCutting.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRental.Producer
{
    public interface IRabbitMQClient
    {
        void Publish(Producer.Producers.Publish pub);
    }
}
