using Motorcycle.Domain.Core.Commands;
using Motorcycle.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Interfaces.Commons
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T pEvent) where T : Event;
        Task SendCommand<T>(T sCommand) where T : Command;

       
    }
}
