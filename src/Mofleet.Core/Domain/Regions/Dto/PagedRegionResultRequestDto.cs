using Abp.Application.Services.Dto;

namespace Mofleet.Domain.Regions.Dto
{
    public class PagedRegionResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public int? CityId { get; set; }
        public bool? IsActive { get; set; }
    }
}
