using AutoMapper;
using Mofleet.Cities.Dto;
using Mofleet.Domain.Cities;

namespace Mofleet.Cities.Mapper
{
    public class CityMapProfile : Profile
    {
        public CityMapProfile()
        {
            CreateMap<CreateCityDto, City>();
            CreateMap<CreateCityDto, CityDto>();
            CreateMap<CityDto, City>();
            CreateMap<UpdateCityDto, City>();
            CreateMap<LiteCity, City>();

        }
    }
}
