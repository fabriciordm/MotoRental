using Motorcycle.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Commands.DeliveryDriver
{
    public class DeleteDeliveryDriverCommand: Command
    {
        public DeleteDeliveryDriverCommand(int id)
        {
            Id = id;
        }
    }
}
