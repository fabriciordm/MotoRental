using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Motorcycle.Domain.Models
{
    public class DeliveryDriver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Identificador { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NumeroCNH { get; set; }
        public string TipoCNH { get; set; }
        public string ImagemCNH { get; set; }
    }

    public enum TipoCNH
    {
        A,
        B,
        AB,
        C
      
    }

}
