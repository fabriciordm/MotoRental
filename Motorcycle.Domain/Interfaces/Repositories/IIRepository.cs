using Motorcycle.Domain.Core.Commands;
using Motorcycle.Domain.Core.Events;
using Motorcycle.Domain.Core.Models;
using Motorcycle.Domain.Interfaces.Commons;
using Motorcycle.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Domain.Interfaces.Repositories
{
    public interface IMotorcycleRepository : IRepository<Moto>
       
    {
        void Add(Moto moto);

        int GetIdByPlate(string plate);

        bool CheckPlateRegistered(string plate);

        bool CheckCnhRegistered( string cnh);

        bool CheckCnpjRegistered(string cnpj);

        bool CheckPlateRegisteredById(int id);

        bool CheckRental(int id);

        bool checRentalCurrent(int idcliente, int idlocacao);
     

        void SaveEvent<T>(T pEvent, string user) where T : Event;
        void Add(DeliveryDriver deliveryObj);
        void Add(Rental rental);

        bool VerifyValidDriverLicence(int id);

        bool VerifyValidDriverLicenceByCnh(string cnh);

        void UpdateDeliveryDriverByCnh(string cnh,string path);
    }
}
