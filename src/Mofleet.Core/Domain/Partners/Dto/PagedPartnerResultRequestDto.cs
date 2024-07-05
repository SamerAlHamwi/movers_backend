using Abp.Application.Services.Dto;

namespace Mofleet.Domain.Partners.Dto
{
    public class PagedPartnerResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
