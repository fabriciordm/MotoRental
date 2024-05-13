using Motorcycle.Domain.CommandHandlers;
using Motorcycle.Domain.Commands.Motorcycle;
using Motorcycle.Domain.Commands.Rental;
using Motorcycle.Domain.Core.Commands;
using Motorcycle.Domain.Interfaces;
using Motorcycle.Domain.Interfaces.Commons;
using Motorcycle.Domain.Interfaces.Repositories;
using Motorcycle.Domain.Models;
using MotoRental.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Text;

namespace MotoRental.Services.AppServices
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMediatorHandler _mediator;
         IMotorcycleRepository _imotorcycleRepository;

        public MotorcycleService(IMediatorHandler mediator,
            IMotorcycleRepository imotorcycleRepository)
        {
            _mediator = mediator;
            _imotorcycleRepository = imotorcycleRepository;
        }

        public bool CheckPlateRegistered(string plate)
        {
            return _imotorcycleRepository.CheckPlateRegistered(plate);
        }

        public bool CheckPlateRegisteredById(int id)
        {
            return _imotorcycleRepository.CheckPlateRegisteredById(id);
        }

        public async Task Create(CreateMotorcycleCommand command)
        {
            
            await _mediator.SendCommand(command);
           
        }   
        public async Task<DeleteMotorcycleCommands> Delete(int id)
        {
            var command = new DeleteMotorcycleCommands(id);
            await _mediator.SendCommand(command);

            return command;
        }

        public void SendMessage()
        {
           
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost"
                };
            
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "motorental-queue",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                    string message = "Bem vindo ao RabbitMQ teste da controller em MotoRental";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                        routingKey: "motorental-queue",
                        basicProperties: null,
                        body: body);

                    Console.WriteLine($"[x] Enviada;{message}");
                }
            
        }

        public async Task Update(UpdateRentalCommand command)
        {
            await _mediator.SendCommand(command);
        }

        public async Task Update(UpdateMotorcycleCommand command)
        {
            await _mediator.SendCommand(command);
        }
    }

}
