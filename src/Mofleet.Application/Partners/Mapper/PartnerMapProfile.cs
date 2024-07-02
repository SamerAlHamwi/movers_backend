using AutoMapper;
using Mofleet.Domain.Codes;
using Mofleet.Domain.Codes.Dto;
using Mofleet.Domain.Partners;
using Mofleet.Domain.Partners.Dto;

namespace Mofleet.Partners.Mapper
{
    public class PartnerMapProfile : Profile
    {
        public PartnerMapProfile()
        {

            CreateMap<Partner, CreatePartnerDto>();
            CreateMap<CreatePartnerDto, Partner>();
            CreateMap<Partner, UpdatePartnerDto>();
            CreateMap<UpdatePartnerDto, Partner>();
            CreateMap<PartnerDetailsDto, Partner>();
            CreateMap<Partner, PartnerDetailsDto>();
            CreateMap<LitePartnerDto, Partner>();
            CreateMap<Partner, LitePartnerDto>();
            CreateMap<CreateCodeDto, Code>();
            CreateMap<Code, CodeDto>().ForMember(dest => dest.PhonesNumbers, opt => opt.Ignore()); ;


        }
    }
}
