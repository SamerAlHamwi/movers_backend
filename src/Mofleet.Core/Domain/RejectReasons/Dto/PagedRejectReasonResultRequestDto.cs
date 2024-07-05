using Abp.Application.Services.Dto;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.RejectReasons.Dto
{
    public class PagedRejectReasonResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public PossibilityPotentialClient? PossibilityPotentialClient { get; set; }

    }
}
