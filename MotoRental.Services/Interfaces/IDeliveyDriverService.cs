using Motorcycle.Domain.Commands.DeliveryDriver;
using Motorcycle.Domain.Commands.Motorcycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRental.Services.Interfaces
{
    public interface IDeliveyDriverService
    {
        Task Create(CreateDeliveryDriverCommand command);

        bool CheckCnpjRegistered(string cnh);
        bool CheckCnhRegistered(string cnpj);

        bool VerifyValidDriverLicence(int id);

        bool VerifyValidDriverLicenceByCnh(string cnh);

        void UpdateDeliveryDriverByCnh(string cnh, string path);

    }
}
