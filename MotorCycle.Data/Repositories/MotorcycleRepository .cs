using MediatR;
using Motorcycle.Domain.Core.Commands;
using Motorcycle.Domain.Core.Events;
using Motorcycle.Domain.Core.Models;
using Motorcycle.Domain.Interfaces.Repositories;
using Motorcycle.Domain.Models;
using MotorCycle.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorCycle.Data.Repositories
{
    public class MotorcycleRepository : Repository<Moto>, IMotorcycleRepository
    {
        private readonly MotoContext _context;
        public MotorcycleRepository(MotoContext context) : base(context)
        {
            _context = context;
        }

        //public void Add<T>(T sCommand) where T : Command
        //{          
        //    _context.Set<T>().Add(sCommand);
        //    _context.SaveChanges();
        //}

        public void Add(Moto moto)
        {
            _context.Add(moto);
            _context.SaveChanges();
        }

        public void Add(DeliveryDriver deliveryObj)
        {
            _context.Add(deliveryObj);
            _context.SaveChanges();
        }

        public void Add(Rental rental)
        {
            _context.Add(rental);
            _context.SaveChanges();
        }

        //public bool CheckCnhRegistered(string cnh)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool CheckCnpjRegistered(string cnpj)
        //{
        //    throw new NotImplementedException();
        //}

        public bool CheckCnhRegistered(string cnh)
        {
            bool exists = false;
            var moto = Db.DeliveryDrivers.FirstOrDefault(m => m.NumeroCNH.ToUpper() == cnh);
            exists = moto != null ? true : false;

            return exists;

        }

        public bool CheckCnpjRegistered(string cnpj)
        {
            bool exists = false;
            var moto = Db.DeliveryDrivers.FirstOrDefault(m => m.CNPJ.ToUpper() == cnpj);
            exists = moto != null ? true : false;

            return exists;

        }

        public void SaveEvent<T>(T pEvent, string user) where T : Event
        {
            throw new NotImplementedException();
        }
    }
}
