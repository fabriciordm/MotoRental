using MediatR;
using Motorcycle.Domain.Core.Commands;
using Motorcycle.Domain.Core.Events;
using Motorcycle.Domain.Interfaces.Commons;
using Motorcycle.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IUser _user;
        private readonly IMotorcycleRepository _repository;

        public MediatorHandler(IUser user, IMediator mediator, IMotorcycleRepository repository)
        {
            _user = user;
            _mediator = mediator;
            _repository = repository;
        }
        public async Task SendCommand<T>(T sCommand) where T : Command
        {           
            await _mediator.Publish(sCommand);
        }
        public async Task PublishEvent<T>(T pEvent) where T : Event
        {
            if (!pEvent.MessageType.Equals("DomainNotification") && pEvent.Save)
                _repository.SaveEvent(pEvent, _user.GetUsrId().ToString());

            await _mediator.Publish(pEvent);
        }
    }
}
