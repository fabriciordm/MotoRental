using Motorcycle.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Commands.Motorcycle
{

    public class DeleteMotorcycleCommands : Command
    {
        public DeleteMotorcycleCommands(int id)
        {
            Id = id;
        }

       
    }
}
