using Motorcycle.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Motorcycle.Domain.Commands.Rental
{
    public class CreateRentalCommand: Command
    {
        public CreateRentalCommand( int idCliente, int idMotocicleta, DateTime dataInicio, DateTime dataFim, decimal preco, string observacoes)
        {
           
            IdCliente = idCliente;
            IdMotocicleta = idMotocicleta;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Preco = preco;
            Observacoes = observacoes;
        }
        public CreateRentalCommand()
        {

        }

        public int IdCliente { get; set; }
        public int IdMotocicleta { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal Preco { get; set; }
        public string Observacoes { get; set; }
    }
}
