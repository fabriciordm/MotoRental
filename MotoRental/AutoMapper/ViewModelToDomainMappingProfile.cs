using AutoMapper;
using Motorcycle.Domain.Commands.DeliveryDriver;
using Motorcycle.Domain.Commands.Motorcycle;
using Motorcycle.Domain.Commands.Rental;
using Motorcycle.Domain.Models;
using MotoRental.API.ViewModels;

namespace MotoRental.API.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<MotoViewModel, Moto>();
            CreateMap<UpdateRentalCommand, Moto>();
            CreateMap<CreateMotorcycleCommand, MotoViewModel>();
            CreateMap<CreateMotorcycleCommand, Moto>();
            CreateMap<MotoViewCompletedModel, Moto>();
            CreateMap<MotoViewUpdatePlate,UpdateRentalCommand>();

           
            CreateMap<DeliveryDriverViewModel,CreateDeliveryDriverCommand>();
            CreateMap<DeliveryDriver,CreateDeliveryDriverCommand>();


            CreateMap<Rental ,CreateRentalCommand>();
            CreateMap<RentalViewModel,CreateRentalCommand>();
            CreateMap<RentalViewModelPriceLess,CreateRentalCommand>();
            CreateMap<Rental,UpdateRentalCommand>();

            CreateMap<Rental,DeleteRentalCommand>();
            CreateMap<RentalViewModelPriceLess, DeleteRentalCommand>();
            CreateMap<UpdateMotorcycleCommand,CreateMotorcycleCommand>();
            CreateMap<UpdateMotorcycleCommand,MotoViewUpdatePlate>();
            CreateMap<UpdateMotorcycleCommand,Moto>();
        }
    }
}
