using Motorcycle.Domain.Commands.DeliveryDriver;
using Motorcycle.Domain.Interfaces.Commons;
using Motorcycle.Domain.Interfaces.Repositories;
using MotoRental.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRental.Services.AppServices
{
    public class DeliveyDriverService: IDeliveyDriverService
    {
        private readonly IMediatorHandler _mediator;
        IMotorcycleRepository _imotorcycleRepository;

        public DeliveyDriverService(IMediatorHandler mediator,
            IMotorcycleRepository imotorcycleRepository)
        {
            _mediator = mediator;
            _imotorcycleRepository = imotorcycleRepository;
        }

        public bool CheckCnhRegistered(string cnh)
        {
            return _imotorcycleRepository.CheckCnhRegistered(cnh);
        }

        public bool CheckCnpjRegistered(string cnpj)
        {
            return _imotorcycleRepository.CheckCnpjRegistered(cnpj);
        }

        public async Task Create(CreateDeliveryDriverCommand command)
        {
            await _mediator.SendCommand(command);
        }

        public void UpdateDeliveryDriverByCnh(string cnh, string path)
        {
            _imotorcycleRepository.UpdateDeliveryDriverByCnh(cnh,path);
        }

        public bool VerifyValidDriverLicence(int id)
        {
            return _imotorcycleRepository.VerifyValidDriverLicence(id);
        }

        public bool VerifyValidDriverLicenceByCnh(string cnh)
        {
            return _imotorcycleRepository.VerifyValidDriverLicenceByCnh(cnh);
        }
    }
}
