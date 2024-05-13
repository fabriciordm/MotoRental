using Motorcycle.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Interfaces.Commons
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
        bool CommitWithNotify(string key = null);

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}

