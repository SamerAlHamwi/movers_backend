using Abp.Application.Services.Dto;

namespace Mofleet.Domain.AttributesForSourceType.Dto
{
    public class PagedAttributeForSourceTypeResultRequestDto : PagedResultRequestDto
    {
        public string KeyWord { get; set; }
        public int? SourceTypeId { get; set; }
        public bool? IsActive { get; set; }

    }
}
