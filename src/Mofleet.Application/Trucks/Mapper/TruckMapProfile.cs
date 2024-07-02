using AutoMapper;
using Mofleet.Domain.Trucks;
using Mofleet.Domain.Trucks.Dto;


namespace Mofleet.Trucks.Mapper
{
    public class TruckMapProfile : Profile
    {
        public TruckMapProfile()
        {
            CreateMap<CreateTruckDto, Truck>();
            CreateMap<CreateTruckDto, TruckDto>();
            CreateMap<TruckDto, Truck>();
            CreateMap<UpdateTruckDto, Truck>();
            CreateMap<LiteTruckDto, Truck>();
            CreateMap<Truck, TruckDetailsDto>();
            CreateMap<Truck, LiteTruckDto>();
        }
    }
}
