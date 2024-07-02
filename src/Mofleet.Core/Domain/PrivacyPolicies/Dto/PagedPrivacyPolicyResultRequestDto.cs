using Abp.Application.Services.Dto;
using static Mofleet.Enums.Enum;

namespace Mofleet.PrivacyPolicyService.Dto
{
    public class PagedPrivacyPolicyResultRequestDto : PagedResultRequestDto
    {
        public bool IsForMoney { get; set; } = false;
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public AppType? App { get; set; }


    }
}
