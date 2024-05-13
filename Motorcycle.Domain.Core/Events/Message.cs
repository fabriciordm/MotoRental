using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Motorcycle.Domain.Core.Events
{
    public abstract class Message : INotification
    {
        public string MessageType { get; protected set; }
        public long AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }

}
