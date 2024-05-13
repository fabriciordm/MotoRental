using Microsoft.EntityFrameworkCore;
using Motorcycle.Domain.Commands.Motorcycle;
using Motorcycle.Domain.Commands.Rental;
using Motorcycle.Domain.Core.Models;
using Motorcycle.Domain.Interfaces.Commons;
using Motorcycle.Domain.Models;
using MotorCycle.Data.Context;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MotorCycle.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected MotoContext Db;
        protected DbSet<TEntity> DbSet;

        protected Repository(MotoContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual void Update(TEntity obj)
        {
            DetachLocal(obj);
            DbSet.Update(obj);
        }
        private void DetachLocal(TEntity entity)
        {
            var local = this.DbSet.Local.FirstOrDefault(entry => entry.Id == entity.Id);

            if (!(local == null))
                Db.Entry(local).State = EntityState.Detached;

            Db.Entry(entity).State = EntityState.Modified;
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string include = null)
        {
            IQueryable<TEntity> Query = DbSet;

            if (!string.IsNullOrEmpty(include))
                foreach (string inc in include.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                    Query = Query.Include(inc);

            return Query.AsNoTracking().Where(predicate);
        }
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int page, int size, string include = null)
        {
            if (string.IsNullOrEmpty(include))
                return DbSet.AsNoTracking().Where(predicate).Skip(page * size).Take(size);
            else
                return DbSet.AsNoTracking().Include(include).Where(predicate).Skip(page * size).Take(size);
        }
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int page, int size, out int maxPage, out int totalItens, string order = "asc")
        {
            var data = DbSet.AsNoTracking().Where(predicate);

            maxPage = 0;
            decimal dMaxPage = Math.Floor((decimal)(data.Count() / (size == 0 ? 1 : size))) + 1;
            int.TryParse(dMaxPage.ToString(), out maxPage);

            totalItens = data.Count();
            var result = data.Skip(page * size).Take(size);

            return order == "asc" ? result.OrderBy(x => x) : result.OrderByDescending(x => x);
        }
     
        public virtual TEntity GetById(int id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking().ToList();
        }

        public virtual void Remove(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public int GetIdByPlate(string plate)
        {
               int id = Db.Motos.Where(moto => moto.Placa == plate)
              .Select(moto => moto.Identificador)
              .FirstOrDefault();

            return id;
        }

        public virtual IEnumerable<Rental> GetAllRentals()
        {
            return Db.Rentals.ToList();           
        }

        public virtual Rental GetAllRentalById(int idLocacao)
        {
            Rental r = new Rental();
            r = Db.Rentals.FirstOrDefault(x => x.IdLocacao == idLocacao);
            return r;
        }


        public virtual void UpdatePlate(UpdateMotorcycleCommand command)
        {
            var moto = Db.Motos.FirstOrDefault(m => m.Identificador == command.Identificador);
            if (moto != null)
            {                
                moto.Placa = command.Placa;                
                Db.SaveChanges();
            }
        }

        public bool CheckPlateRegistered(string plate)
        {
            bool exists = false;
            var moto = Db.Motos.FirstOrDefault(m=>m.Placa.ToUpper() == plate);
            exists = moto !=null ? true : false;

            return exists;

        }

        public bool checRentalCurrent(int idcliente, int IdMotocicleta)
        {
            bool exists = false;
            var moto = Db.Rentals.FirstOrDefault(m => m.IdCliente == idcliente && m.IdMotocicleta == IdMotocicleta);
            exists = moto != null ? true : false;

            return exists;
        }

        public bool CheckPlateRegisteredById(int id)
        {
            bool exists = false;
            var moto = Db.Motos.FirstOrDefault(m => m.Identificador == id);
            
            exists = moto != null ? true : false;

            return exists;
        }


        public bool CheckRental(int id)
        {
        
        bool exists = false;
        var moto = Db.Rentals.FirstOrDefault(m => m.IdMotocicleta == id);
        exists = moto != null ? true : false;

            return exists;
        }
       

    public bool VerifyValidDriverLicence(int id)
        {
            bool exists = false;
            var moto = Db.DeliveryDrivers.FirstOrDefault(m => m.Identificador == id);
            if (moto != null) {
                exists = (moto.TipoCNH.ToUpper().Contains("A")) ? true : false;
            }
            else
            {
                exists = false;
            }
            return exists;
        }

    public bool VerifyValidDriverLicenceByCnh(string cnh)
    {
        bool exists = false;
        var moto = Db.DeliveryDrivers.FirstOrDefault(m => m.NumeroCNH == cnh);
        
       exists = moto == null ? false:true;
        /// exists = (!string.IsNullOrEmpty(moto.NumeroCNH)) ? true : false;

        return exists;
    }

    public void UpdateDeliveryDriverByCnh(string cnh, string path)
        {
            var deliveryDriver = Db.DeliveryDrivers.FirstOrDefault(m => m.NumeroCNH == cnh);
            if (deliveryDriver != null)
            {
                // Atualizando a placa da moto
                deliveryDriver.ImagemCNH = path;

                // Salvando as mudanças no banco de dados
                Db.SaveChanges();
            }
        }

     public void RemoveRental(int id)
        {
            Rental r = new Rental();
            r.IdLocacao = id;
            Db.Rentals.Remove(r);
        }

      
    }
}
