using AutoMapper;
using Mofleet.Domain.Toolss;
using Mofleet.Domain.Toolss.Dto;

namespace Mofleet.AttributesForSourceType.Mapper
{
    public class ToolMapProfile : Profile
    {
        public ToolMapProfile()
        {
            CreateMap<CreateToolDto, Tool>();
            CreateMap<UpdateToolDto, Tool>();
            CreateMap<LiteToolDto, Tool>();
            CreateMap<ToolDetailsDto, Tool>();

        }
    }
}
