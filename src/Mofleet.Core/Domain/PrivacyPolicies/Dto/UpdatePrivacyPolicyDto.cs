using Abp.Application.Services.Dto;

namespace Mofleet.PrivacyPolicyService.Dto
{
    public class UpdatePrivacyPolicyDto : CreatePrivacyPolicyDto, IEntityDto
    {
        public int Id { get; set; }
    }
}