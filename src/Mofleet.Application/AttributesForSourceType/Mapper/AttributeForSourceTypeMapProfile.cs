using AutoMapper;
using Mofleet.Domain.AttributesForSourceType;
using Mofleet.Domain.AttributesForSourceType.Dto;

namespace Mofleet.AttributesForSourceType.Mapper
{
    public class AttributeForSourceTypeMapProfile : Profile
    {
        public AttributeForSourceTypeMapProfile()
        {
            CreateMap<CreateAttributeForSourceTypeDto, AttributeForSourceType>();
            CreateMap<UpdateAttributeForSourceTypeDto, AttributeForSourceType>();
            CreateMap<LiteAttributeForSourceTypeDto, AttributeForSourceType>();
            CreateMap<AttributeForSourceTypeDetailsDto, AttributeForSourceType>();

        }
    }
}
