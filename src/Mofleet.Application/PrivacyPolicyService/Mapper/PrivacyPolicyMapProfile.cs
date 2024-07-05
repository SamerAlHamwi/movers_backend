using AutoMapper;
using Mofleet.Domain.PrivacyPolicies;
using Mofleet.PrivacyPolicyService.Dto;

namespace Mofleet.PrivacyPolicyService.Mapper
{
    public class PrivacyPolicyMapProfile : Profile
    {
        public PrivacyPolicyMapProfile()
        {
            CreateMap<CreatePrivacyPolicyDto, PrivacyPolicy>();
        }
    }
}
