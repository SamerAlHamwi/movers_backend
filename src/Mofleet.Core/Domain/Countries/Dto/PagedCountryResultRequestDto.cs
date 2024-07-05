using Abp.Application.Services.Dto;
using static Mofleet.Enums.Enum;

namespace Mofleet.Countries.Dto
{
    public class PagedCountryResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public ServiceType? Type { get; set; }

    }
}
