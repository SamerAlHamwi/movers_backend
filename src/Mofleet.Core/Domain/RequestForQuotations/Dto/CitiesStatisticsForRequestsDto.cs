using Mofleet.Cities.Dto;

namespace Mofleet.Domain.RequestForQuotations.Dto
{
    public class CitiesStatisticsForRequestsDto
    {
        public int CityId { get; set; }
        public LiteCityDto cityDto { get; set; }
        public int RequestForQuotationCount { get; set; }
    }
}
