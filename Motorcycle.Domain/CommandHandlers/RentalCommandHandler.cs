using AutoMapper;
using MediatR;
using Motorcycle.Domain.Commands.Motorcycle;
using Motorcycle.Domain.Commands.Rental;
using Motorcycle.Domain.Core.Notifications;
using Motorcycle.Domain.Handlers;
using Motorcycle.Domain.Interfaces.Commons;
using Motorcycle.Domain.Interfaces.Repositories;
using Motorcycle.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.CommandHandlers
{
    public class RentalCommandHandler : CommandHandler,
      INotificationHandler<CreateRentalCommand>,
      INotificationHandler<DeleteRentalCommand>,
      INotificationHandler<UpdateRentalCommand>


    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IMotorcycleRepository _motorcycleRepository;

        public RentalCommandHandler(
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

        public Task Handle(CreateRentalCommand notification, CancellationToken cancellationToken)
        {
            var motoObj = _mapper.Map<Rental>(notification);
            _motorcycleRepository.Add(motoObj);
            _motorcycleRepository.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Handle(DeleteRentalCommand notification, CancellationToken cancellationToken)
        {
            var motoObj = _mapper.Map<Rental>(notification);
            _motorcycleRepository.RemoveRental(motoObj.IdLocacao);
            _motorcycleRepository.SaveChanges();
            return Task.CompletedTask;
        }

      

        public Task Handle(UpdateRentalCommand notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
