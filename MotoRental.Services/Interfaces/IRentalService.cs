using Motorcycle.Domain.Commands.DeliveryDriver;
using Motorcycle.Domain.Commands.Rental;
using Motorcycle.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRental.Services.Interfaces
{
    public interface IRentalService
    {
        Task Create(CreateRentalCommand command);
        bool CheckRental(int id);

        string CalculaRentalValue(DateTime begin, DateTime end);

        bool checRentalCurrent(int idcliente, int IdMotocicleta);

        Task DeleteRental(DeleteRentalCommand command);

        IEnumerable<Rental> GetAllRentals();

        Rental GetAllRentalById(int idLocacao);

        decimal calculateInterest(DateTime begin, DateTime end);
    }
}
