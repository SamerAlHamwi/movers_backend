using Abp.Application.Services.Dto;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.MoneyTransfers.Dtos
{
    public class PagedMoneyTransferResultRequestDto : PagedResultRequestDto
    {
        public ReasonOfPaid? ReasonOfPaid { get; set; }
        public PaidStatues? PaidStatues { get; set; }
        public PaidProvider? PaidProvider { get; set; }
    }
}
