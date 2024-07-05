using Abp.Application.Services.Dto;

namespace Mofleet.Domain.Drafts.Dtos
{
    public class PagedDraftResultRequestDto : PagedResultRequestDto
    {
        public long? UserId { get; set; }
    }

}
