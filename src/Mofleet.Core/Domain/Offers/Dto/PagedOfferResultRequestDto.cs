using Abp.Application.Services.Dto;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.Offers.Dto
{
    public class PagedOfferResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public OfferStatues? Statues { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyBranchId { get; set; }
        public bool MyOffers { get; set; } = false;
        public long? RequestId { get; set; }

    }
}
