using MediatR;
using Motorcycle.Domain.Commands.Motorcycle;
using Motorcycle.Domain.Core.Notifications;
using Motorcycle.Domain.Interfaces;
using Motorcycle.Domain.Interfaces.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Handlers
{
    public abstract class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notifications;

        public IUnitOfWork Uow { get; }
        public IMediatorHandler Mediator { get; }
        public INotificationHandler<DomainNotification> Notifications { get; }

        protected CommandHandler(IUnitOfWork uow, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }
     
        protected bool Commit(bool transaction)
        {
            if (transaction)
                return true;

            if (_notifications.HasNotifications()) return false;

            var commandResponse = _uow.Commit();
            if (commandResponse.Success) return true;

            Console.WriteLine("Ocorreu um erro ao salvar os dados no banco");
            _mediator.PublishEvent(new DomainNotification("Commit", "Ocorreu um erro ao salvar os dados no banco"));
            return false;
        }

    }
}
