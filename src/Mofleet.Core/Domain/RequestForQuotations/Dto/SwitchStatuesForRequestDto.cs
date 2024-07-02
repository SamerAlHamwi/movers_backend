using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.RequestForQuotations.Dto
{
    public class SwitchStatuesForRequestDto
    {
        public long RequestId { get; set; }
        public RequestForQuotationStatues Statues { get; set; }
        public string ReasonRefuse { get; set; }
    }
}
