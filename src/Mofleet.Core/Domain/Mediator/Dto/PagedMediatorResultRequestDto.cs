using Abp.Application.Services.Dto;

namespace Mofleet.Mediators.Dto
{
    public class PagedMediatiorResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
