using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.RequestForQuotations.Dto
{
    public class RequestCompanyCompanyBranchIdsDto
    {
        public long RequestId { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyBranchId { get; set; }
        public OfferStatues OfferStatues { get; set; }
    }
}
