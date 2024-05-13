using Motorcycle.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Commands.Motorcycle
{
    public class SearchMotorcycleCommands : Command
    {
        public SearchMotorcycleCommands(string plate) {
            Placa = plate;
        }

        public int Identificador { get; set; }
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
    }
}
