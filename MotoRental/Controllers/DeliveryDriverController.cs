using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Motorcycle.Domain.Commands.DeliveryDriver;
using Motorcycle.Domain.Commands.Motorcycle;
using Motorcycle.Domain.Interfaces.Repositories;
using MotoRental.API.ViewModels;
using MotoRental.Services.Interfaces;
using System.Diagnostics.Eventing.Reader;

namespace MotoRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryDriverController : ControllerBase
    {
        private readonly IMotorcycleService _imotorcycleService;
        private IMotorcycleRepository _imotorcycleRepository;
        private readonly IDeliveyDriverService _ideliveyDriverService;
        private readonly IRentalService _rentalService;
        private readonly IConfiguration _iconfiguration;

        private readonly IMapper _imapper;

        public DeliveryDriverController(IMotorcycleService imotorcycleService,
            IDeliveyDriverService deliveyDriverService,
           IMotorcycleRepository imotorcycleRepository,
           IRentalService rentalService,
           IConfiguration configuration,
           IMapper _mapper)
        {
            _imotorcycleService = imotorcycleService;
            _imotorcycleRepository = imotorcycleRepository;
            _imapper    = _mapper;
            _ideliveyDriverService = deliveyDriverService;
            _rentalService = rentalService;
            _iconfiguration = configuration;
        }

        [HttpPost("CreateDeliveryDriver")]
        public async Task<IActionResult> CreateMotorcycle(DeliveryDriverViewModel driver)
        {
            bool verifyCnhExists = _ideliveyDriverService.CheckCnhRegistered(driver.NumeroCNH);
            bool verifyCNpj = _ideliveyDriverService.CheckCnpjRegistered(driver.CNPJ);

            if (!verifyCnhExists || !verifyCNpj)
            {

                if (driver.TipoCNH.ToUpper() == "A" ||
                   driver.TipoCNH.ToUpper() == "B" ||
                   driver.TipoCNH.ToUpper() == "AB")
                {
                    if (!_ideliveyDriverService.CheckCnhRegistered(driver.NumeroCNH.ToUpper()))
                        if (!_ideliveyDriverService.CheckCnpjRegistered(driver.CNPJ.ToUpper()))
                        {
                            var command = _imapper.Map<CreateDeliveryDriverCommand>(driver);
                            await _ideliveyDriverService.Create(command);
                        }
                        else
                        {
                            return BadRequest($"O Usuário {driver.Nome} Já se encontra cadastradado");
                        }
                }
                else
                {
                    return BadRequest($"Categoria de cnh Inválida");
                }
                return Ok();
            }
            else
            {
                return BadRequest("CNPJ e/ou CNH existentes.");
            }
           
        }

        

        [HttpGet("ConsultarRentalValue")]
        public string ConsultarRentalValue(DateTime begin, DateTime end)
        {
            string price = _rentalService.CalculaRentalValue(begin, end);
            if (Convert.ToDecimal(price) <= 0)
                return ("Por favor digite um periOdo de locação válido entre 7,15,30,45, ou 50 dias");
            else
                return price;
        }


        [HttpPut("uploadCnh")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Upload(IFormFile file, string cnh)
        {
            bool exists = _ideliveyDriverService.VerifyValidDriverLicenceByCnh(cnh);
            if (exists)
            {
                if (file == null)
                {
                    return BadRequest("Faça upload da cnh.");
                }             
                var extension = Path.GetExtension(file.FileName)?.ToUpper();
                         
                if (extension.Contains("PNG") || extension.Contains("BMP"))
                {
                    var uploadDirectory = _iconfiguration.GetValue<string>("FileStorage:UploadDirectory");                  
                    var filePath = Path.Combine(uploadDirectory, file.FileName);                    
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    
                   _ideliveyDriverService.UpdateDeliveryDriverByCnh(cnh,filePath);
                }

                else
                {
                    return BadRequest("Faça upload da cnh no formatp png ou bmp");
                }
            }


            else
            {
                return BadRequest("Cnh não encontrada.");
            }
            
            return Ok("Dados atualizados");
        }
    }
}
