using AutoMapper;
using Motorcycle.Domain.Commands.DeliveryDriver;
using Motorcycle.Domain.Commands.Motorcycle;
using Motorcycle.Domain.Commands.Rental;
using Motorcycle.Domain.Models;
using MotoRental.API.ViewModels;
using Motorcycle.Domain.Commands.Motorcycle;

namespace MotoRental.API.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Moto, MotoViewModel>();
            CreateMap<Moto, UpdateRentalCommand>();
            CreateMap<MotoViewModel, CreateMotorcycleCommand>();
            CreateMap<Moto, CreateMotorcycleCommand>();
            CreateMap<Moto, MotoViewCompletedModel>();
            CreateMap<UpdateRentalCommand, MotoViewUpdatePlate>();

            CreateMap<DeliveryDriver, DeliveryDriverViewModel>();
            CreateMap<CreateDeliveryDriverCommand, DeliveryDriver>();
            CreateMap<CreateDeliveryDriverCommand,DeliveryDriverViewModel>();

            CreateMap<CreateRentalCommand, Rental>();
            CreateMap<CreateRentalCommand, RentalViewModel>();
            CreateMap<CreateRentalCommand, RentalViewModelPriceLess>();

            CreateMap<UpdateRentalCommand, RentalViewModelPriceLess>();
            CreateMap<UpdateRentalCommand, Rental>();

            CreateMap<DeleteRentalCommand, Rental>();
            CreateMap<DeleteRentalCommand,RentalViewModelPriceLess>();

            CreateMap<IEnumerable<object>, Rental>();
            CreateMap<CreateMotorcycleCommand, UpdateMotorcycleCommand>();
            CreateMap<MotoViewUpdatePlate, UpdateMotorcycleCommand>();
            CreateMap<Moto, UpdateMotorcycleCommand>();
          
        }
    }
}
