using Motorcycle.Domain.Commands.Rental;
using Motorcycle.Domain.Core.Commands;
using Motorcycle.Domain.Interfaces.Commons;
using Motorcycle.Domain.Interfaces.Repositories;
using Motorcycle.Domain.Models;
using MotoRental.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRental.Services.AppServices
{
    public class RentalService : IRentalService
    {
        private readonly IMediatorHandler _mediator;
        IMotorcycleRepository _imotorcycleRepository;
       

        public RentalService(IMediatorHandler mediator,
            IMotorcycleRepository imotorcycleRepository)
        {
            _mediator = mediator;
            _imotorcycleRepository = imotorcycleRepository;
        }

        public string CalculaRentalValue(DateTime begin, DateTime end)
        {
            decimal price = 0;
            DateTime dataInicial = begin;
            DateTime dataFinal = end;

            TimeSpan intervalo = dataFinal - dataInicial;
            int dias = (int)intervalo.TotalDays;

            switch (dias)
            {
                case 7:
                    price = 30 * 7;
                    break;
                case 15:
                    price = 28 * 15;
                    break;
                case 30:
                    price = 22 * 15;
                    break;
                case 45:
                    price = 20 * 15;
                    break;
                case 50:
                    price = 18 * 15;
                    break;
                default:
                    Console.WriteLine("Intervalo não reconhecido");
                    break;
            }

            return price.ToString();
        }

        public decimal calculateInterest(DateTime end, DateTime change)
        {

            
            decimal price = 0;
            DateTime antecipada = change;
            DateTime dataFinal = end;

            TimeSpan intervalo = dataFinal - antecipada;
            int dias = (int)intervalo.TotalDays;
            dias = Math.Abs(dias);

            decimal seteDias = 30 * 7;
            decimal quinzeDias = 28 * 15;
            decimal trintaDias = 22 * 15;
            decimal quarentaEcincoDias = 20 * 15;
            decimal cinquentaDias = 20 * 15;

            if (antecipada < dataFinal)
            {
                switch (dias)
                {
                    case <= 7:
                        price = seteDias + (seteDias * 20 / 100);
                        break;
                    case <= 15:
                        price = quinzeDias + (quinzeDias * 40 / 100);
                        break;
                    case <= 30:
                        price = trintaDias;
                        break;
                    case <= 45:
                        price = quarentaEcincoDias;
                        break;
                    case <= 50:
                        price = cinquentaDias;
                        break;
                    default:
                        Console.WriteLine("Intervalo não reconhecido");
                        break;
                }
            }
            else
            {
                switch (dias)
                {
                    case <= 7:
                        price = seteDias + 50;
                        break;
                    case <= 15:
                        price = quinzeDias + 50;
                        break;
                    case <= 30:
                        price = trintaDias + 50;
                        break;
                    case <= 45:
                        price = quarentaEcincoDias + 50;
                        break;
                    case <= 50:
                        price = cinquentaDias + 50;
                        break;
                    default:
                        Console.WriteLine("Intervalo não reconhecido");
                        break;
                }
            }
           
            return price;
        }
        public bool CheckRental(int id)
        {
            return _imotorcycleRepository.CheckRental(id);
        }

        public async Task checRentalCurrent(UpdateRentalCommand command)
        {
            await _mediator.SendCommand(command);
        }

        public bool checRentalCurrent(int idcliente, int IdMotocicleta)
        {
            return _imotorcycleRepository.checRentalCurrent(idcliente, IdMotocicleta);
        }

        public async Task Create(CreateRentalCommand command)
        {
            await _mediator.SendCommand(command);
        }

        public async Task DeleteRental(DeleteRentalCommand command)
        {
            await _mediator.SendCommand(command);
        }

        public Rental GetAllRentalById(int idLocacao)
        {
            Rental r = new Rental();
            r = _imotorcycleRepository.GetAllRentalById(idLocacao);
            return r;
        }

        public  IEnumerable<Rental> GetAllRentals()
        {
          return _imotorcycleRepository.GetAllRentals();          
        }
    }
}
