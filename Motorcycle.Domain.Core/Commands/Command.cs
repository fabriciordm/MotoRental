using Motorcycle.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Core.Commands
{
    public class Command:Message
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; private set; }
        public bool Transaction { get; private set; }

       

        public Command()
        {
            Timestamp = DateTime.Now;
            Transaction = false;
           
        }

        public void SetId(int id)
        {
            Id = id;
        }
        public void SetTransaction(bool transaction)
        {
            Transaction = transaction;
        }

    }
}
