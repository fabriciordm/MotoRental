using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Motorcycle.Domain.Commands.Rental;
using MotoRental.API.Controllers;
using MotoRental.API.ViewModels;
using MotoRental.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRemtal.Tests
{
    public class RentalControllerTests
    {
        [Fact]
        public async Task CreateRental_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var mockDeliveryDriverService = new Mock<IDeliveyDriverService>();
            object value = mockDeliveryDriverService.Setup(x => x.VerifyValidDriverLicence(It.IsAny<int>())).ReturnsAsync(true);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<CreateRentalCommand>(It.IsAny<RentalViewModelPriceLess>())).Returns(new CreateRentalCommand());

            var mockRentalService = new Mock<IRentalService>();
            mockRentalService.Setup(x => x.CalculaRentalValue(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns("100"); // Substitua "100" pelo valor esperado

            var mockIRentalService = new Mock<IRentalService>();
            mockIRentalService.Setup(x => x.Create(It.IsAny<CreateRentalCommand>())).Returns(Task.CompletedTask);

            var controller = new RentalController(mockDeliveryDriverService.Object, mockMapper.Object, mockRentalService.Object, mockIRentalService.Object);

            // Act
            var result = await controller.CreateRental(new RentalViewModelPriceLess());

            // Assert
            Assert.IsType<OkResult>(result);
        }

    }
}
