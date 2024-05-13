using AutoMapper;
using MediatR;
using Motorcycle.Domain.Commands.DeliveryDriver;
using Motorcycle.Domain.Commands.Motorcycle;
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
    public class DeliveryDriverCommandHandler : CommandHandler,
        INotificationHandler<CreateDeliveryDriverCommand>,
        INotificationHandler<SearchDeliveryDriverCommand>
    
    {
        private readonly IMapper _mapper;
        private readonly IMotorcycleRepository _motorcycleRepository;

        public DeliveryDriverCommandHandler(IMapper mapper,
            IUnitOfWork uow,
            IMediatorHandler mediator,
            IMotorcycleRepository motorcycleRepository,
            INotificationHandler<DomainNotification> notifications) : base(uow, mediator, notifications) 
        {
            _mapper = mapper;
            _motorcycleRepository = motorcycleRepository;
        }

        public Task Handle(CreateDeliveryDriverCommand notification, CancellationToken cancellationToken)
        {
            var deliveryObj = _mapper.Map<DeliveryDriver>(notification);
            _motorcycleRepository.Add(deliveryObj);
            _motorcycleRepository.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Handle(SearchDeliveryDriverCommand notification, CancellationToken cancellationToken)
        {
            var deliveryObj = _mapper.Map<DeliveryDriver>(notification);
            _motorcycleRepository.GetById(deliveryObj.Identificador);
            _motorcycleRepository.SaveChanges();
            return Task.CompletedTask;
        }
    }

}
