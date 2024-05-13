using Motorcycle.Domain.Interfaces.Commons;
using Motorcycle.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Interfaces.Repositories
{
    public interface IDeliveryDriverRepository : IRepository<DeliveryDriver>
    {
        void Add(DeliveryDriver driver);
    }
}
