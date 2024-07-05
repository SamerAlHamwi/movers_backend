using Mofleet.Domain.Companies.Dto;
using Mofleet.Domain.CompanyBranches.Dto;
using Mofleet.Domain.RequestForQuotations.Dto;

namespace Mofleet.Domain.SelectedCompaniesByUsers.Dto
{
    public class SelectedCompaniesBySystemForRequestDto
    {
        public RequestForQuotationDto RequestForQuotation { get; set; }
        public CompanyDto Company { get; set; }
        public CompanyBranchDto CompanyBranch { get; set; }
    }
}
