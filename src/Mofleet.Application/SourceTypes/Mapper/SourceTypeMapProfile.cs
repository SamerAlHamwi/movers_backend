using AutoMapper;
using Mofleet.Domain.SourceTypes;
using Mofleet.Domain.SourceTypes.Dto;

namespace Mofleet.SourceTypes.MApper
{
    public class SourceTypeMapProfile : Profile
    {
        public SourceTypeMapProfile()
        {
            CreateMap<CreateSourceTypeDto, SourceType>();

        }
    }
}
