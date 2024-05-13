using Motorcycle.Domain.Commands.Motorcycle;
using Motorcycle.Domain.Commands.Rental;
using Motorcycle.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Interfaces.Commons;

public interface IRepository<TEntity> : IDisposable
{
    void Add(TEntity obj);
    TEntity GetById(int id);
    IEnumerable<TEntity> GetAll();

    IEnumerable<Rental> GetAllRentals();

    Rental GetAllRentalById(int idLocacao);
    void Update(TEntity obj);

    void UpdatePlate(UpdateMotorcycleCommand command);
    void Remove(int id);

    void RemoveRental(int id);
   
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string include = null);
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int page, int size, string include = null);
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int page, int size, out int maxPage, out int totalItens, string order = "asc");

    int SaveChanges();
   
}
