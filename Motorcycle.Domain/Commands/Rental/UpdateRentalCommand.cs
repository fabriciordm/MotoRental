using Motorcycle.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Commands.Rental
{
    public class UpdateRentalCommand : Command
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLocacao { get; set; }
        public int IdCliente { get; set; }
        public int IdMotocicleta { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal Preco { get; set; }
        public string Observacoes { get; set; }
    }
}
