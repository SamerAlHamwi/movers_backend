using Abp.Application.Services.Dto;

namespace Mofleet.Domain.SourceTypes.Dto
{
    public class PagedSourceTypeResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }

    }
}
