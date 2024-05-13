using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Motorcycle.Domain.Commands.Motorcycle;
using Motorcycle.Domain.Commands.Rental;
using Motorcycle.Domain.Core.Events;
using Motorcycle.Domain.Core.Notifications;
using Motorcycle.Domain.Interfaces.Commons;
using Motorcycle.Domain.Interfaces.Repositories;
using Motorcycle.Domain.Models;
using MotorCycle.Data.Repositories;
using MotoRental.API.ViewModels;
using MotoRental.CrossCutting.MessageBus.Rabbitmq;
using MotoRental.Producer.Producers;
using MotoRental.Services.Interfaces;
using Newtonsoft;
using RabbitMQ.Client;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace MotoRental.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MotoController : Controller
    {

        private readonly IMotorcycleService _imotorcycleService;
        private IMotorcycleRepository _imotorcycleRepository;
        private IRentalService _rentalService;
        private readonly IMapper _mapper;

        public object JsonConvert { get; private set; }       
        public MotoController(IMotorcycleService imotorcycleService,
            IMediatorHandler mediator,
            IMotorcycleRepository motorcycleRepository,
            IMapper mapper,
            IRentalService rentalService,
            IOptions<Producer.RabbitSettings> options, 
            ILogger<Producer.RabbitMQClient> logger,           
            INotificationHandler<DomainNotification> notifications)
        {
            _imotorcycleService = imotorcycleService;
            _imotorcycleRepository = motorcycleRepository;
            _mapper = mapper;
            _rentalService = rentalService;                               
        }

        
       
        [HttpPost("CreateMotorcycle")]
        public async Task<IActionResult> CreateMotorcycle(MotoViewModel moto)
        {
            bool Ano2024 = false;
            if (!_imotorcycleService.CheckPlateRegistered(moto.Placa.ToUpper()))
            {
                var command = _mapper.Map<CreateMotorcycleCommand>(moto);
               
                if(command.Ano == 2024)
                {
                    Ano2024= true;
                    command.Modelo = command.Modelo + " ***Moto Ano de 2024***";
                }

                await _imotorcycleService.Create(command);
               
             
                string CreateMotorcycle = JsonSerializer.Serialize(moto);
                var rabbitMqService = new RabbitMqService("localhost", "motorental-queue");

                if(Ano2024)
                rabbitMqService.SendMessage("Moto Ano de 2024");               
                
                rabbitMqService.SendMessage(CreateMotorcycle);
              
              
            }
            else
            {
                return NotFound($"A Placa {moto.Placa} Já se encontra cadastrada.");
            }
            return Ok();
        }

        
        [HttpDelete("{id}")]       
        public async Task<IActionResult> RemoveMotorcycle(int id)
        {

            if (!_rentalService.CheckRental(id))
            {
                DeleteMotorcycleCommands command = new DeleteMotorcycleCommands(id);               
                await _imotorcycleService.Delete(command.Id);
            }
           else
            {
                return NotFound($"Não é possivel deletar este cadasro, pois possui locação");
            }

            return Ok();
        }

       
        [HttpGet]
        [Route("SearchByRegistrationPlate")]
        public IActionResult SearchByRegistrationPlate(string plate)
        {

            var motorcycle = _imotorcycleRepository.Find(x => x.Placa.ToUpper() == plate.ToUpper()).FirstOrDefault();
            if (motorcycle != null)
            {
                var motoObj = _mapper.Map<MotoViewCompletedModel>(motorcycle);

                string motorcycleSerialized = JsonSerializer.Serialize(motoObj);
               
                return Ok(motorcycleSerialized);
            }
            else
            {
                return NotFound("Placa não encontrada");
            }
        }

        [HttpGet]
        [Route("GetMotorcycles")]
        public IActionResult GetMotorcycles()
        {
            var motorcycles = _imotorcycleRepository.GetAll();

            if (motorcycles != null && motorcycles.Any())
            {               
                var motorcycleViewModels = _mapper.Map<List<MotoViewCompletedModel>>(motorcycles);
              
                return Ok(motorcycleViewModels);
            }
            else
            {
                return NotFound("Nenhuma motocicleta encontrada");
            }
        }

        [HttpPut()]
        [Route("UpdateMotorcycle")]
        public async Task<IActionResult> Update(MotoViewUpdatePlate moto)
        {     
            UpdateMotorcycleCommand command = new UpdateMotorcycleCommand(moto.Identificador, moto.Placa);
            _mapper.Map<UpdateMotorcycleCommand>(moto);
            await _imotorcycleService.Update(command);

            return Ok();
        }



    }
}
