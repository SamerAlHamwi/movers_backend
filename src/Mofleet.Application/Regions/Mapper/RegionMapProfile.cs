using AutoMapper;
using Mofleet.Domain.Regions;
using Mofleet.Domain.Regions.Dto;

namespace Mofleet.Regions.Mapper
{
    public class RegionMapProfile : Profile
    {
        public RegionMapProfile()
        {
            CreateMap<CreateRegionDto, Region>();
            CreateMap<CreateRegionDto, RegionDto>();
            CreateMap<RegionDto, Region>();
            CreateMap<UpdateRegionDto, Region>();
            CreateMap<LiteRegionDto, Region>();
        }
    }
}
