using Abp.Application.Services.Dto;
using Mofleet.Domain.Companies.Dto;
using Mofleet.Domain.CompanyBranches.Dto;
using Mofleet.Domain.UserVerficationCodes;
using static Mofleet.Enums.Enum;

namespace Mofleet.Domain.MoneyTransfers.Dtos
{
    public class MoneyTransferDto : EntityDto
    {
        public LiteUserDto User { get; set; }
        public CompanyDto Company { get; set; }
        public CompanyBranchDto CompanyBranch { get; set; }
        public double Amount { get; set; }
        public ReasonOfPaid ReasonOfPaid { get; set; }
        public PaidStatues PaidStatues { get; set; }
        public PaidProvider PaidProvider { get; set; }
        public PaidDestination PaidDestination { get; set; }
    }
}
