using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Motorcycle.Domain.CommandHandlers;
using Motorcycle.Domain.Commands.Motorcycle;
using Motorcycle.Domain.Commands.Rental;
using Motorcycle.Domain.Core.Notifications;
using Motorcycle.Domain.Handlers;
using Motorcycle.Domain.Interfaces;
using Motorcycle.Domain.Interfaces.Commons;
using Motorcycle.Domain.Interfaces.Repositories;
using Motorcycle.Domain.Models;

namespace Motorcycle.Domain.CommandHandlers
{
    public class MotorcycleCommandHandler : CommandHandler,
      INotificationHandler<CreateMotorcycleCommand>,
      INotificationHandler<DeleteMotorcycleCommands>,
      INotificationHandler<UpdateMotorcycleCommand>
     
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IMotorcycleRepository _motorcycleRepository;
        
        public MotorcycleCommandHandler(
            IMotorcycleRepository motorcycleRepository,
            IUnitOfWork uow, 
            IMediatorHandler mediator,
            IMapper mapper,
            INotificationHandler<DomainNotification> notifications) : base(uow, mediator, notifications)
        {
            _mapper = mapper;
            _mediator = mediator;
            _motorcycleRepository = motorcycleRepository;
        }

        public Task Handle(CreateMotorcycleCommand notification, CancellationToken cancellationToken)
        {
           var motoObj = _mapper.Map<Moto>(notification);                 
            _motorcycleRepository.Add(motoObj);
            _motorcycleRepository.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Handle(DeleteMotorcycleCommands notification, CancellationToken cancellationToken)
        {
            _motorcycleRepository.Remove(notification.Id);

            if (Commit(notification.Transaction))
            {
                _motorcycleRepository.SaveChanges();
            }
           
            return Task.CompletedTask;
        }



        public Task Handle(UpdateMotorcycleCommand notification, CancellationToken cancellationToken)
        {

            _motorcycleRepository.UpdatePlate(notification);
            _motorcycleRepository.SaveChanges();
            return Task.CompletedTask;
        }

    }

}
