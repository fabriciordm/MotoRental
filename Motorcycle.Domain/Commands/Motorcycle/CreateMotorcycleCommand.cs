using MediatR;
using Motorcycle.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Commands.Motorcycle
{
    public class CreateMotorcycleCommand : Command
    {

        public CreateMotorcycleCommand( int ano, string modelo, string placa)
        {
            //Identificador = identificador;
            Ano = ano;
            Modelo = modelo;
            Placa = placa.ToUpper();
        }

        //public int Identificador { get; set; }
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
    }
}
