using Motorcycle.Domain.Core.Commands;
using Motorcycle.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Commands.DeliveryDriver
{
    public class SearchDeliveryDriverCommand : Command
    {
        public SearchDeliveryDriverCommand(string nome, string cnpj, DateTime dataNascimento, string numeroCNH, string tipoCNH)
        {
            Nome = nome;
            CNPJ = cnpj.ToUpper();
            DataNascimento = dataNascimento;
            NumeroCNH = numeroCNH.ToUpper();
            TipoCNH = tipoCNH;
        }

        public int Identificador { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NumeroCNH { get; set; }
        public string TipoCNH { get; set; }
        public string ImagemCNH { get; set; }
    }
}
