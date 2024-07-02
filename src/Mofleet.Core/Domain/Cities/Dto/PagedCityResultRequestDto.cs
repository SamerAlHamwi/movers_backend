using Abp.Application.Services.Dto;

namespace Mofleet.Cities.Dto
{
    public class PagedCityResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public int? CountryId { get; set; }
        public bool? IsActive { get; set; }
    }
}
