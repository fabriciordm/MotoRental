using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motorcycle.Domain.Models;
using System.ComponentModel;

namespace MotoRental.API.ViewModels
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryDriverViewModel 
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NumeroCNH { get; set; }
        public string TipoCNH { get; set; }
        
        [DefaultValue("Prezado cliente, será disponibilizada a funcionalidade de upload do seu documento CNH (formatos png ou bmp) durante a atualização.")]
        public string ImagemCNH { get; set; }
    }
}
