using AutoMapper;
using Mofleet.Domain.RejectReasons;
using Mofleet.Domain.RejectReasons.Dto;

namespace Mofleet.RejectReasons.Mapper
{
    public class RejectReasonMapProfile : Profile
    {
        public RejectReasonMapProfile()
        {
            CreateMap<CreateRejectReasonDto, RejectReason>();
            CreateMap<UpdateRejectReasonDto, RejectReason>();
            CreateMap<RejectReasonTranslationDto, RejectReasonTranslation>();
        }
    }
}
