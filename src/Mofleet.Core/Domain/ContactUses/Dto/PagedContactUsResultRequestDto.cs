using Abp.Application.Services.Dto;

namespace Mofleet.ContactUsService.Dto
{
    public class PagedContactUsResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
