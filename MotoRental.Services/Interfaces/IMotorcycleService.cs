using Motorcycle.Domain.CommandHandlers;
using Motorcycle.Domain.Commands.Motorcycle;
using Motorcycle.Domain.Commands.Rental;
using Motorcycle.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRental.Services.Interfaces
{
   public interface IMotorcycleService
    {
       Task Create(CreateMotorcycleCommand command);
       Task<DeleteMotorcycleCommands> Delete(int id);
       
       Task Update(UpdateMotorcycleCommand command);

       bool CheckPlateRegistered(string plate);
       bool CheckPlateRegisteredById(int id);

        void SendMessage();


    }
}
