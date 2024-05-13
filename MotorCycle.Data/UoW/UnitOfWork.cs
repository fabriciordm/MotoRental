using MediatR;
using Motorcycle.Domain.Core.Commands;
using Motorcycle.Domain.Core.Notifications;
using Motorcycle.Domain.Interfaces.Commons;
using MotorCycle.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRental.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MotoContext _context;
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notifications;

        public UnitOfWork(MotoContext context, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications)
        {
            _context = context;
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public CommandResponse Commit()
        {
            var rowsAffected = _context.SaveChanges();
            return new CommandResponse(rowsAffected > 0);
        }
        public bool CommitWithNotify(string key = null)
        {
            if (_notifications.HasNotifications(key)) return false;

            var commandResponse = Commit();
            if (commandResponse.Success) return true;

            _mediator.PublishEvent(new DomainNotification("Commit", "There was an error saving the data"));
            return false;
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            _context.Database.CurrentTransaction.Commit();
        }
        public void RollbackTransaction()
        {
            _context.Database.CurrentTransaction.Rollback();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
