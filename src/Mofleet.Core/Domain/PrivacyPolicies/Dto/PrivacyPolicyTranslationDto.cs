using Abp.AutoMapper;
using Mofleet.Domain.PrivacyPolicies;

namespace Mofleet.PrivacyPolicyService.Dto
{
    [AutoMap(typeof(PrivacyPolicyTranslation))]

    public class PrivacyPolicyTranslationDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }

    }
}
