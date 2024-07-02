using AutoMapper;
using Mofleet.Domain.AttributeChoices;
using Mofleet.Domain.AttributeChoices.Dto;

namespace Mofleet.AttributeChoices.Mapper
{
    public class AttributeChoiceMapProfile : Profile
    {
        public AttributeChoiceMapProfile()
        {
            CreateMap<CreateAttributeChoiceDto, AttributeChoice>();
            CreateMap<CreateAttributeChoiceDto, AttributeChoiceDto>();
            CreateMap<AttributeChoiceDto, AttributeChoice>();
            CreateMap<AttributeChoice, UpdateAttributeChoiceDto>();
            CreateMap<UpdateAttributeChoiceDto, AttributeChoice>();
            //CreateMap<AttributeChoiceDto, LiteAttributeChoiceDto>();
        }



    }
}
