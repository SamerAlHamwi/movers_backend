using Abp.Application.Services.Dto;
using static Mofleet.Enums.Enum;

namespace Mofleet.TermService.Dto
{
    public class PagedTermResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public AppType? App { get; set; }

    }
}
