using AutoMapper;
using Mofleet.Domain.Drivers;
using Mofleet.Domain.Drivers.Dto;
using Mofleet.Domain.Drivers.Dto;
using Mofleet.Domain.Drivers;

namespace Mofleet.Drivers.Mapper
{
    public class DriverMapProfile : Profile
    {
        public DriverMapProfile()
        {
            CreateMap<CreateDriverDto, Driver>();
            CreateMap<CreateDriverDto, DriverDto>();
            CreateMap<DriverDto, Driver>();
            CreateMap<UpdateDriverDto, Driver>();
            CreateMap<LiteDriverDto, Driver>();
            CreateMap<Driver, DriverDetailsDto>();
            CreateMap<Driver, LiteDriverDto>();
        }
    }
}
