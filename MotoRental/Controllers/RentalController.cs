using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motorcycle.Domain.Commands.Motorcycle;
using Motorcycle.Domain.Commands.Rental;
using Motorcycle.Domain.Core.Notifications;
using Motorcycle.Domain.Interfaces.Commons;
using Motorcycle.Domain.Interfaces.Repositories;
using Motorcycle.Domain.Models;
using MotoRental.API.ViewModels;
using MotoRental.Services.Interfaces;

namespace MotoRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IMotorcycleService _imotorcycleService;
        private readonly IRentalService _irentalService;
        private readonly IMotorcycleRepository _imotorcycleRepository;
        private readonly IDeliveyDriverService _deliveryDriverService;
        private readonly IRentalService _rentalService;        
        private readonly IMapper _mapper;

        public object JsonConvert { get; private set; }

        public RentalController(IMotorcycleService imotorcycleService,
            IRentalService irentalService,
            IMediatorHandler mediator,
            IMotorcycleRepository motorcycleRepository,
            IMapper mapper,
            IDeliveyDriverService deliveryDriverService,
            IRentalService rentalService,         
            INotificationHandler<DomainNotification> notifications)
        {
            _imotorcycleService = imotorcycleService;
            _imotorcycleRepository = motorcycleRepository;
            _mapper = mapper;
            _irentalService = irentalService;
            _deliveryDriverService = deliveryDriverService;
            _rentalService = rentalService;
           
        }

        [HttpPost("createrental")]
        public async Task<IActionResult> CreateRental(RentalViewModelPriceLess locacao)
        {

            bool existsDriverLicence = _deliveryDriverService.VerifyValidDriverLicence(locacao.IdCliente);
            bool existsIdMotorcycle = _imotorcycleService.CheckPlateRegisteredById(locacao.IdMotocicleta);
            
           
            bool existsRental = _rentalService.checRentalCurrent(locacao.IdCliente, locacao.IdMotocicleta);

            if (!existsRental)
            {
                if (!existsDriverLicence && !existsIdMotorcycle)
                {
                    return BadRequest("Não foi localizado um contrato de locação. Verifique se os dados do Condutor e da Motocicleta estão corretos.");
                }

                if (!existsIdMotorcycle && existsDriverLicence)
                {
                    return BadRequest("A motocicleta especificada não está registrada.");
                }

                if (existsIdMotorcycle && !existsDriverLicence)
                {
                    return BadRequest("Condutor não encontrado.");
                }

                if (existsDriverLicence && existsIdMotorcycle)
                {
                    var command = _mapper.Map<CreateRentalCommand>(locacao);

                    string price = _rentalService.CalculaRentalValue(locacao.DataInicio, locacao.DataFim);

                    command.Preco = Convert.ToDecimal(price);

                    if (command.Preco > 0)
                    {
                        await _irentalService.Create(command);

                        return Ok();
                    }

                    else
                    {
                        return BadRequest("Por favor digite um perido de locação válido entre 7,15,30,45, ou 50 dias");
                    }

                }
                else
                {
                    return BadRequest("Não foi localizada a categoria A para este condutor.");
                }
            }

            else
            {
                return BadRequest("Cadastro existente");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveRental(int id)
        {
            if (!_rentalService.CheckRental(id))
            {
                DeleteRentalCommand command = new DeleteRentalCommand(id);
                // await _imotorcycleService.Delete(command.Id);
                await _rentalService.DeleteRental(command);
            }
            else
            {
                return NotFound($"Não é possivel deletar este cadasro, pois o mesmo não existe");
            }

            return Ok();
        }

        [HttpGet]
        [Route("GetMotorcycles")]
        public IActionResult GetRentals()
        {
            var rentals = _rentalService.GetAllRentals();
          

            if (rentals != null && rentals.Any())
            {              
                return Ok(rentals);
            }
            else
            {
                return NotFound("Nenhuma motocicleta encontrada");
            }
        }

        [HttpGet]
        [Route("earlyRental")]       
        public IActionResult earlyRental(int id , DateTime datAalterada)
        {
          RentalViewModelEarly rt = new RentalViewModelEarly();
           rt.IdLocacao = id;
          
            var rental = _rentalService.GetAllRentalById(rt.IdLocacao);
         
            var rentalObj= _mapper.Map<Rental>(rental);
      
            decimal result = _rentalService.calculateInterest(rentalObj.DataFim, datAalterada); // retorna respectivamente dias e preço      
        
            return Ok($"O valor acrescido de juros é de R${result}");
        }
    }
}
