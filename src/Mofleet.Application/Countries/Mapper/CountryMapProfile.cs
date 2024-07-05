using AutoMapper;
using Mofleet.Countries.Dto;

namespace Mofleet.Countries.Mapper
{
    public class CountryMapProfile : Profile
    {
        public CountryMapProfile()
        {
            CreateMap<CreateCountryDto, Country>();
            CreateMap<CreateCountryDto, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Country, UpdateCountryDto>();
            CreateMap<UpdateCountryDto, Country>();
        }
    }
}
