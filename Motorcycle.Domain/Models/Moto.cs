using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Motorcycle.Domain.Core.Models;

namespace Motorcycle.Domain.Models
{
    public class Moto : Entity<Moto>
    {
        public Moto()
        {
            
        }
        public Moto(int identificador, int ano, string modelo, string placa)
        {
            Identificador = identificador;
            Ano = ano;  
            Modelo = modelo;
            Placa = placa;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Identificador { get; set; }
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }

       
       


        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }

}
