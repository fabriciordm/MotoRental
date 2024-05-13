using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Motorcycle.Domain.Commands.Rental;
using Motorcycle.Domain.Interfaces.Repositories;
using MotoRental.API.Controllers;
using MotoRental.API.ViewModels;
using MotoRental.Services.Interfaces;
using NUnit.Framework;

namespace MotoRental.Tests
{
    [TestFixture]
    public class RentalControllerTests
    {
        private RentalController _controller;
        private Mock<IRentalService> _rentalServiceMock;
        private Mock<IMotorcycleService> _motorcycleServiceMock;
        private Mock<IDeliveyDriverService> _deliveryDriverServiceMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void SetUp()
        {
            _rentalServiceMock = new Mock<IRentalService>();
            _deliveryDriverServiceMock = new Mock<IDeliveyDriverService>();
            _motorcycleServiceMock = new Mock<IMotorcycleService>(); // Adicionado esta linha
            _mapperMock = new Mock<IMapper>();

            _controller = new RentalController(
                imotorcycleService: null,
                irentalService: _rentalServiceMock.Object,
                mediator: null,
                motorcycleRepository: null,
                mapper: _mapperMock.Object,
                deliveryDriverService: _deliveryDriverServiceMock.Object,
                rentalService: _rentalServiceMock.Object,
                notifications: null
            );
        }


        [Test]
        public async Task CreateRental_WithValidData_ReturnsOk()
        {
            try
            {
                // Arrange
                var rentalViewModel = new RentalViewModelPriceLess
                {
                    IdCliente = 2,
                    IdMotocicleta = 10,
                    DataInicio = DateTime.Now.AddDays(1), // Data de início no futuro
                    DataFim = DateTime.Now.AddDays(7) // Data de fim 7 dias após a data de início
                };

                _deliveryDriverServiceMock.Setup(x => x.VerifyValidDriverLicence(It.IsAny<int>())).Returns(true);
                _motorcycleServiceMock.Setup(x => x.CheckPlateRegisteredById(It.IsAny<int>())).Returns(true);
                _rentalServiceMock.Setup(x => x.checRentalCurrent(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
                _mapperMock.Setup(x => x.Map<CreateRentalCommand>(It.IsAny<RentalViewModelPriceLess>())).Returns(new CreateRentalCommand());
                _rentalServiceMock.Setup(x => x.CalculaRentalValue(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns("100"); // Preço qualquer

                // Act
                var result = await _controller.CreateRental(rentalViewModel) as OkResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode); // Verifica se o status code é 200 (OK)
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw; // Re-throw a exceção para que o teste falhe
            }
        }

    }
}
