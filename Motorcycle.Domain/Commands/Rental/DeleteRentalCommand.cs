using Motorcycle.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Commands.Rental
{
    public class DeleteRentalCommand : Command
    {
        public DeleteRentalCommand(int idLocacao, int idcliente, int idmotocicleta, DateTime datainicio, DateTime datafim, decimal preco, string observacoes)
        {
           

            IdCliente = idcliente;
            IdMotocicleta = idmotocicleta;
            DataInicio = datainicio;
            DataFim = datafim;
            Preco = preco;
            Observacoes = observacoes;
        }

        public DeleteRentalCommand(int idLocacao)
        {
            
            IdLocacao = idLocacao;          
        }

        public int IdLocacao { get; set; }
        public int IdCliente { get; set; }
        public int IdMotocicleta { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal Preco { get; set; }
        public string Observacoes { get; set; }

    }
}
