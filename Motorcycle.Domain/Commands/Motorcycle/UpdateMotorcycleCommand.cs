using Motorcycle.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Commands.Rental
{
    public class UpdateMotorcycleCommand : Command
    {
       
        public UpdateMotorcycleCommand(int identificador, int ano, string modelo, string placa)
        {
            Identificador = identificador;
            Ano = ano;
            Modelo = modelo;
            Placa = placa.ToUpper();
        }
        public UpdateMotorcycleCommand(int identificador, string placa)
        {
            Identificador = identificador;
            Placa = placa.ToUpper();
        }
        public UpdateMotorcycleCommand()
        {

        }

        public int Identificador { get; set; }
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
    }
}
